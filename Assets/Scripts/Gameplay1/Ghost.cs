using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class Ghost: IntEventInvoker
{
    private bool isGrounded = false;
    private bool isDoubleJump = false;
    private float horizontal;
    private float speed = 22f;
    private float jumpingPower = 40f;

    [SerializeField]
    public int health = 4;
    public int pegouDoubleJump;
    public int pegouShotter;
    private Animator anim;
    private SpriteRenderer sr;
    private FieldOfView fieldOfView;
    public Vector3 position;
    private Timer shotCountdown;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject atackPrefab;
    [SerializeField] private GameObject shotPrefab;

    


    private void Start()
    {
        // add as event invoker for events
        unityIntEvents.Add(EventName.HealthChangedEvent, new HealthChangedEvent());
        EventManager.AddIntInvoker(EventName.HealthChangedEvent, this);
        unityIntEvents.Add(EventName.GameOverEvent, new GameOverEvent());
        EventManager.AddIntInvoker(EventName.GameOverEvent, this);
        EventManager.AddIntListener(EventName.GetDoubleJumpEvent, HandleGetDoubleJumpEvent);
        EventManager.AddIntListener(EventName.GetShotterEvent, HandleGetShotterEvent);
        EventManager.AddIntListener(EventName.TakeDamageEvent,TakeDamage);


        fieldOfView = GetComponent<FieldOfView>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        shotCountdown = gameObject.AddComponent<Timer>();
        shotCountdown.Duration = 5;
        shotCountdown.Run();
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

        if(pegouShotter== 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && shotCountdown.Finished)
            {
                StartCoroutine(ShotterAtack(0.8f));
            }

        }

        if (isGrounded)
        {
            rb.gravityScale = 9f;
            anim.SetBool("jump", false);
            unityIntEvents[EventName.HealthChangedEvent].Invoke(health);
        }
        else
        {
            anim.SetBool("moving", false);
            unityIntEvents[EventName.HealthChangedEvent].Invoke(health);
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
        health = health - damage;
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
    void HandleGetShotterEvent(int value)
    {
        pegouShotter = value;
    }
    private IEnumerator ShotterAtack(float segundos)
    {
        anim.SetTrigger("shotting");
        shotCountdown.Run();
        yield return new WaitForSeconds(segundos); 

        if (!sr.flipX)
        {
            GameObject newShot = Instantiate(shotPrefab, new Vector2(gameObject.transform.position.x + 4.5f, gameObject.transform.position.y + 2), Quaternion.identity);
            newShot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 25, ForceMode2D.Impulse);
            
        }
        else
        {
            GameObject newShot = Instantiate(shotPrefab, new Vector2(gameObject.transform.position.x - 5, gameObject.transform.position.y + 2), Quaternion.identity);
            newShot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 25, ForceMode2D.Impulse);
            newShot.GetComponent<SpriteRenderer>().flipX = true;
            
        }
    }
}


