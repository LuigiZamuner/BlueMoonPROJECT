using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBoss : IntEventInvoker
{
    [SerializeField]
    GameObject portalPrefab;
    [SerializeField]
    private float atackCooldown;
    private int damage = 1;
    private int bossHealth = 12;
    private float cooldowntimer = Mathf.Infinity;

    FieldOfView fieldOfView;
    private Transform playerPos;
    private Animator anim;
    Follow follow;

    private SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        follow = GetComponent<Follow>();
        sr = GetComponent<SpriteRenderer>();
        fieldOfView = GetComponent<FieldOfView>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Start()
    {
        follow.SetImpulseForce(0);
        unityIntEvents.Add(EventName.TakeDamageEvent, new TakeDamageEvent());
        EventManager.AddIntInvoker(EventName.TakeDamageEvent, this);
    }
    private void Update()
    {
        cooldowntimer += Time.deltaTime;
        if (fieldOfView.FieldOfViewEnemy())
        {
            follow.SetImpulseForce(10);
            anim.SetTrigger("moving");
            if (playerPos.position.x > transform.position.x)
            {
                sr.flipX = false;
            }
            else if (playerPos.position.x < transform.position.x)
            {
                sr.flipX = true;
            }
        }
        if (fieldOfView.PlayerContact())
        {
            int randomAtack = Random.Range(-2, 1);

            if (cooldowntimer >= atackCooldown )
            {
                cooldowntimer = 0;

                if(randomAtack == -1)
                {
                    anim.SetTrigger("atack1");
                }
                else if(randomAtack == 0)
                {
                    anim.SetTrigger("atack2");
                }
                else
                {
                    anim.SetTrigger("atack3");
                }

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            bossHealth = bossHealth - 1;
            anim.SetTrigger("getHit");
            Destroy(other.gameObject);
            if (bossHealth == 0)
            {
                Destroy(gameObject);
                Instantiate(portalPrefab, new Vector2(477,-30), Quaternion.identity);

            }
        }
    }
    private void DamagePlayer()
    {
        if (fieldOfView.PlayerContact())
        {
            unityIntEvents[EventName.TakeDamageEvent].Invoke(damage);
        }
    }
}


