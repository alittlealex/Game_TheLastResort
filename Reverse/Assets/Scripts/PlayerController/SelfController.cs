using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfController : MonoBehaviour {

    //variable for how fast player runs//
    public float speed = 5f;

    private bool facingRight = true;
    private Animator anim;
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    //variable for how high player jumps//
    [SerializeField]
    private float jumpForce = 300f;

    private Rigidbody2D rb;

    bool dead = false;
    bool attack = false;

    public enum CtrlState
    {
        Player = 0,        //玩家可移动状态
        NonPlayer = 1      //过场动画状态
    }
    //当前状态
    public CtrlState ctrlState = CtrlState.Player;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        anim = GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        if(ctrlState == CtrlState.Player)
            HandleInput();
    }
    
    //movement//
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        //Debug.Log(grounded);
        if (ctrlState == CtrlState.Player)
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (!attack)
            {
                anim.SetFloat("vSpeed", rb.velocity.y);
                anim.SetFloat("Speed", Mathf.Abs(horizontal));
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            }
            if (horizontal > 0 && !facingRight && !dead && !attack)
            {
                Flip(horizontal);
            }

            else if (horizontal < 0 && facingRight && !dead && !attack)
            {
                Flip(horizontal);
            }
        }
    }

    //attacking and jumping//
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.K) && !dead)
        {
            attack = true;
            anim.SetTrigger("attack");
            anim.SetFloat("Speed", 0);
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            attack = false;
            //anim.SetBool("attack", false);
        }

        if (grounded && Input.GetKeyDown(KeyCode.Space) && !dead)
        {
            anim.SetBool("Ground", false);
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    public void setDead()
    {
        anim.SetBool("Dead", true);
        dead = true;
    }

    private void Flip(float horizontal)
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
