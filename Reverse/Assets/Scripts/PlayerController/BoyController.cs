using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyController : MonoBehaviour {

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    //目标速度
    private Vector2 targetVelocity;
    //是否在地上
    private bool grounded;
    //是否死亡
    private bool dead = false;
    //是否在攻击
    //private bool attack = false;
    //地面的法线
    private Vector2 groundNormal;
    //刚体
    private Rigidbody2D rb2d;
    //速度
    private Vector2 velocity;
    private ContactFilter2D contactFilter;
    //可能会碰到的物体数组
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    //可能会碰到的物体list
    private List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    //最短移动距离
    private const float minMoveDistance = 0.001f;
    private const float shellRadius = 0.01f;
    //最大速度
    public float maxSpeed = 7;
    //最大跳跃距离
    public float jumpMaxHeight = 7;
    //玩家控制状态和过场动画状态
    public enum CtrlState
    {
        Player = 0,        //玩家可移动状态
        NonPlayer = 1      //过场动画状态
    }
    //当前状态
    public CtrlState ctrlState = CtrlState.Player;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        rb2d = transform.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        if (ctrlState == CtrlState.Player)
        {
            ComputeBehaviour();
        }
        else
        {
            if (!dead)
            {
                animator.SetBool("Ground", grounded);
                animator.SetFloat("Speed", Mathf.Abs(velocity.x) / maxSpeed);
            }
        }
    }

    private void ComputeBehaviour()
    {
        //移动方向
        Vector2 move = Vector2.zero;
        //移动的x方向
        move.x = Input.GetAxis("Horizontal");
        //如果按跳跃键且在地面上，就给velocity一个向上的速度
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpMaxHeight;
        }
        //如果抬起跳跃键，就将velocity向上速度减半
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }
        //如果贴图方向和移动方向相反则反向
        if (move.x > 0.01f)
        {
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        }
        else if (move.x < -0.01f)
        {
            if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }
        //设置当前控制移动速度的x值
        targetVelocity = move * maxSpeed;
        if (!dead)
        {
            animator.SetBool("Ground", grounded);
            animator.SetFloat("Speed", Mathf.Abs(velocity.x) / maxSpeed);
        }

        if (Input.GetKeyDown(KeyCode.K) && !dead)
        {
            animator.SetTrigger("attack");
        }
    }

    void FixedUpdate()
    {
        //每帧给其一个向下的速度（可能在后面抵消掉）
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        //x方向速度是上一帧的x速度
        velocity.x = targetVelocity.x;
        //先让其不在地面上，在后面判断
        grounded = false;
        //当前速度要走到的位置
        Vector2 deltaPosition = velocity * Time.deltaTime;
        //当前沿着地面的方向
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        //x方向的移动
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        //移动的距离
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            //计算碰撞体要撞到的物体
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }
            //遍历每个碰撞体将碰到的东西
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                //碰到的东西的碰撞点的法向量
                Vector2 currentNormal = hitBufferList[i].normal;
                //如果法向量的y 比较大就看作是地面
                if (currentNormal.y > minGroundNormalY)
                {
                    //认为现在要碰到地面
                    grounded = true;
                    //如果当前是计算y方向的碰撞，当前的法向量是地面的法向量
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                else
                {
                    currentNormal = Vector2.zero;
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }
                //更改后的移动距离
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

    //过场动画时设置速度
    public void setVelocity()
    {
        animator.SetBool("Ground", true);
        if(spriteRenderer.flipX == false)
            targetVelocity.x = maxSpeed/2;
        else
            targetVelocity.x = -maxSpeed / 2;
    }

    //过场动画时设置攻击
    public void setAttack()
    {
        animator.SetTrigger("attack");
    }

   
}
