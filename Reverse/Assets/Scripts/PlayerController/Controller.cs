using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserActor
{
    public string name { get; set; }
    public Vector2 position { get; set; }
    public long LifeState { get; set; }
    public long jump { get; set; }
    public long walk { get; set; }
    public long attack { get; set; }
    public int gender { get; set; }

    public UserActor() { }
    public UserActor(string _name, Vector2 _position, int _LifeState, int _jump, int _walk, int _attack, int _gender)
    {
        name = _name;
        position = _position;
        LifeState = _LifeState;
        jump = _jump;
        walk = _walk;
        attack = _attack;
        gender = _gender;
    }

    public static bool operator ==(UserActor a, UserActor b)
    {
        bool ret = false;
        if (a.name == b.name && a.LifeState == b.LifeState && a.jump == b.jump && a.walk == b.walk && a.attack == b.attack && a.gender == b.gender)
        {
            ret = true;
        }
        return ret;
    }

    public static bool operator !=(UserActor a, UserActor b)
    {
        bool ret = true;
        if (a.name == b.name && a.LifeState == b.LifeState && a.jump == b.jump && a.walk == b.walk && a.attack == b.attack && a.gender == b.gender)
        {
            ret = false;
        }
        return ret;
    }
}

public class Controller : MonoBehaviour {

    private GameObject Tools;
    public string mName;
    public int gender;
    public int isDoingAttack;
    //计时器
    private float timer;
    private bool isStill = false;
    //每帧根据这个来计算怎么走
    public UserActor actor;
    public UserActor newActor;
    public Transform groundCheck;
    //public Transform wallCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    //获取的组件
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    //变量
    public float jumpMaxForce = 250;
    public float walkSpeed = 5;
    public float gravityModifier = 1.0f;

    private Vector2 velocity;

    private Vector2 targetVelocity;
    //是否在地上
    private bool grounded;
    //是否死亡
    public bool dead;

    private Scene scene;

    public AudioSource soundEffects;
    public AudioClip attackAudio;
    public AudioClip jumpAudio;
    public AudioClip deadAudio;

    //玩家控制状态和过场动画状态
    public enum CtrlState
    {
        Player = 0,        //玩家可移动状态
        NonPlayer = 1      //过场动画状态
    }
    //当前状态
    public CtrlState ctrlState = CtrlState.Player;

    
    void Start ()
    {
        scene = SceneManager.GetActiveScene();
        isDoingAttack = -1;
        timer = 0;
        dead = false;
        Tools = GameObject.Find("Tools");
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //新建的actor为玩家最初的状态，位置在最开始的地方，活，不跳，静止，不攻击
        actor = new UserActor(mName, gameObject.GetComponent<Rigidbody2D>().position,1,-1,0,-1,gender);
	}
	
	void Update ()
    {
        //如果自己不动，就开始记时
        if (isStill && gender == Network.m_Actor.gender)
            timer += Time.deltaTime;
        else
            timer = 0;

        if(timer >= 1.5)
        {
            Tools.GetComponent<Tools>().SendSetPosition(rb.position);
            timer = 0;
        }
        //如果收到了新的setposition
        if (Network.m_Actor.isReceiveSetPosition)
        {
            if(gender != Network.m_Actor.gender)
            {
                rb.position = Network.m_Actor.newSetPosition;
                actor.position = rb.position;
                Network.m_Actor.isReceiveSetPosition = false;
            }
        }
	}

    void FixedUpdate()
    {
        newActor = ComputeBehaviour();
        if (newActor.LifeState == -1)
        {
            //soundEffects.clip = deadAudio;
            //soundEffects.Play();
            SendNewMessage(newActor);
        }
        //如果新的actor和旧的actor不一样，就发生改变，就发送UserActor_req到服务器
        if (actor != newActor)
        {
            SendNewMessage(newActor);
        }
        //当从服务器接到新的UserActor_ack之后，更新actor
        if (Network.m_Actor.isReceiveActorAck)
        {
            for(int i = 0; i < Network.m_Actor.actorAckList.Count;i++)
            {
                //如果性别与自己一致，就赋值给自己的player
                if (Network.m_Actor.actorAckList[i].gender == gender)
                {
                    //将新的操作赋值给actor
                    SetNewActor(Network.m_Actor.actorAckList[i]);
                    Network.m_Actor.actorAckList.Remove(Network.m_Actor.actorAckList[i]);
                }
            }
            //如果所有的数据包都被接受完毕了
            if (Network.m_Actor.actorAckList.Count == 0)
            {
                Network.m_Actor.actorAckList.Clear();
                //再将是否收到改为false
                Network.m_Actor.isReceiveActorAck = false;
            }
        }

        //根据actor计算出下一帧的位置和其他信息
        ComputeInfomation();
        //使用新的actor来决定当前状态人物的动画
        DoActor();
        //更新actor中的位置值
        actor.position = rb.position;
    }

    void SetNewActor(mmopb.UserActor_ack userActorAck)
    {
        actor.name = userActorAck.name;
        Vector2 pos = new Vector2(userActorAck.position.x, userActorAck.position.y);
        actor.position = pos;
        if((rb.position - actor.position).magnitude > 0.5)
            rb.position = actor.position;
        actor.LifeState = userActorAck.LifeState;
        //如果在地上，才能再跳
        if(grounded)
            actor.jump = userActorAck.action.jump;
        actor.walk = userActorAck.action.walk;
        if(actor.attack == -1)
            actor.attack = userActorAck.action.attack;
    }

    UserActor ComputeBehaviour() 
    {
        UserActor ret = new UserActor();
        if (gender == Network.m_Actor.gender && ctrlState == CtrlState.Player)
        {
            
            ret.name = mName;
            ret.LifeState = 1;
            //左跳
            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.W))
            {
                ret.walk = -1;
                ret.jump = 1;
                ret.attack = -1;
                ret.position = rb.position;
                ret.gender = gender;
                
                isStill = false;
            }
            //右跳
            else if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.W))
            {
                ret.walk = 1;
                ret.jump = 1;
                ret.attack = -1;
                ret.position = rb.position;
                ret.gender = gender;
                //soundEffects.clip = jumpAudio;
                //soundEffects.Play();
                isStill = false;
            }
            //左走
            else if (Input.GetKey(KeyCode.A))
            {
                ret.walk = -1;
                ret.jump = -1;
                ret.attack = -1;
                ret.position = rb.position;
                ret.gender = gender;
                isStill = false;
            }
            //右走
            else if (Input.GetKey(KeyCode.D))
            {
                ret.walk = 1;
                ret.jump = -1;
                ret.attack = -1;
                ret.position = rb.position;
                ret.gender = gender;
                isStill = false;
            }
            //上跳
            else if (Input.GetKeyDown(KeyCode.W))
            {
                ret.walk = 0;
                ret.jump = 1;
                ret.attack = -1;
                ret.position = rb.position;
                ret.gender = gender;
                isStill = false;
            }
            //互动
            else if (Input.GetKeyDown(KeyCode.K))
            {
                ret.walk = 0;
                ret.jump = -1;
                ret.attack = 1;
                ret.position = rb.position;
                ret.gender = gender;
                isStill = false;
            }
            //静止
            else
            {
                ret.walk = 0;
                ret.jump = -1;
                ret.attack = -1;
                ret.position = rb.position;
                ret.gender = gender;
                isStill = true;
            }
            
        }
        else
        {
            ret = actor;
        }

        //死了
        if (dead)
        {
            ret.walk = 0;
            ret.jump = -1;
            ret.attack = -1;
            ret.position = rb.position;
            ret.gender = gender;
            ret.LifeState = -1;
            isStill = true;
        }

        return ret;
    }

    void SendNewMessage(UserActor _actor)
    {
        mmopb.UserActor_req req = new mmopb.UserActor_req();
        req.name = _actor.name;
        req.position = new mmopb.Vector2();
        req.position.x = _actor.position.x;
        req.position.y = _actor.position.y;
        req.LifeState = _actor.LifeState;
        req.action = new mmopb.Action();
        req.action.jump = _actor.jump;
        req.action.walk = _actor.walk;
        req.action.attack = _actor.attack;
        req.gender = _actor.gender;
        req.RoomID = Network.m_Actor.inRoomNum;
        Tools.GetComponent<Tools>().SendUserActorReq(req);
    }

    //计算下一帧的位置和其他信息,设置actor的position为下一帧的位置
    void ComputeInfomation()
    {
        //检测是否在地上
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //Debug.Log(grounded);

        if(actor.jump == 1/* && grounded*/)
        {
            rb.AddForce(new Vector2(0, jumpMaxForce)); //添加一个向上的力
            soundEffects.clip = jumpAudio;
            soundEffects.Play();
            actor.jump = -1;
        }
        if(actor.walk != 0)
        {
            rb.velocity -= new Vector2(rb.velocity.x, 0);//先将rb速度的x值置为0
            rb.velocity += new Vector2(actor.walk * walkSpeed, 0);
        }
        else
        {
            rb.velocity -= new Vector2(rb.velocity.x, 0);
        }
    }


    //当前帧执行什么动画
    void DoActor()
    {
        //根据walk的方向决定图片朝向
        if (actor.walk > 0)
        {
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        }
        else if (actor.walk < 0)
        {
            if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }
        //如果只是走，就播放 走的动画
        if(actor.walk != 0 && grounded)
        {
            anim.SetFloat("Speed", Math.Abs(actor.walk));
        }
        //如果不动，就不动
        else if(actor.walk == 0 && grounded)
        {
            anim.SetFloat("Speed", actor.walk);
        }

        //如果是跳，就播放 跳的动画,并让actor中的jump失效
        anim.SetBool("Ground", grounded);
       
        //如果是互动，就播放 互动的动画,并让actor中的attack失效
        if (actor.attack == 1)
        {
            anim.SetTrigger("attack");
            soundEffects.clip = attackAudio;
            soundEffects.Play();
            if (gender == Network.m_Actor.gender)
            {
                isDoingAttack = 1;
            }
            actor.attack = -1;
        }

        //如果死了，就播放 死的动画
        if (actor.LifeState == -1)
        {
            ctrlState = CtrlState.NonPlayer;
            anim.SetBool("Dead", true);
            
            Invoke("ReStart", 2);
        }
    }

    void ReStart()
    {
        SceneManager.LoadScene(scene.name);
    }
}


