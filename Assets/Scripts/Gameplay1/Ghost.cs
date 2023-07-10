using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Ghost: IntEventInvoker
{
    private bool isGrounded = false;
    private bool isDoubleJump = false;
    private float horizontal;
    private float speed = 22f;
    private float jumpingPower = 40f;
    public int health = 3;
    public int pegouDoubleJump;
    private Animator anim;
    private SpriteRenderer sr;
    private BoxCollider2D bcd2;
    private FieldOfView fieldOfView;
    public Vector3 position;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject atackPrefab;

    private void Start()
    {
        // add as event invoker for events
        unityIntEvents.Add(EventName.HealthChangedEvent, new HealthChangedEvent());
        EventManager.AddIntInvoker(EventName.HealthChangedEvent, this);
        unityIntEvents.Add(EventName.GameOverEvent, new GameOverEvent());
        EventManager.AddIntInvoker(EventName.GameOverEvent, this);
        EventManager.AddIntListener(EventName.GetDoubleJumpEvent, HandleGetDoubleJumpEvent);
        EventManager.AddIntListener(EventName.TakeDamageEvent,TakeDamage);

        fieldOfView = GetComponent<FieldOfView>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        bcd2 = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            anim.SetBool("moving",true);
            position.x += horizontal * speed *Time.deltaTime;
            if(horizontal > 0)
            {
                sr.flipX = false;
            }
            else if (horizontal < 0)
            {
                sr.flipX = true;
            }
        }
        else
        {
            anim.SetBool("moving", false);
        }
        transform.position = position;
        if (Input.GetButtonDown("Fire1") && anim.GetBool("moving") == false)
        {
            anim.SetTrigger("idleAtack");
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("jump",true);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isGrounded = false;
            isDoubleJump = true;

        }
        else if (pegouDoubleJump == 1)
        {
            if (Input.GetButtonDown("Jump") && isDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 30);
                isDoubleJump = false;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {

            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            
        }

        if (isGrounded)
        {
            rb.gravityScale = 9f;
            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("moving", false);
        }

    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isDoubleJump= false;
            anim.SetTrigger("falling");
        }

    }
    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        unityIntEvents[EventName.HealthChangedEvent].Invoke(health);

        // check for game over
        if (health == 0 || health < 0)
        {
            MenuManager.GoToMenu(MenuName.GameOver);
        }
    }
    private void AtackDamage()
    {
        if (fieldOfView.PlayerContact())
        {
            Instantiate(atackPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }
    void HandleGetDoubleJumpEvent(int value)
    {
        pegouDoubleJump = value;
    }

}


