using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Skeleton : IntEventInvoker
{
    [SerializeField]
    GameObject skeletonDeathPrefab;
    [SerializeField]
    GameObject coinPrefab;
    [SerializeField]
    private float atackCooldown;
    [SerializeField]
    private int damage;
    private int skeletonHealth = 3;
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
        fieldOfView= GetComponent<FieldOfView>();
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
            follow.SetImpulseForce(6);
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
            if (cooldowntimer >= atackCooldown)
            {
                cooldowntimer = 0;
                anim.SetTrigger("atack");
               
            }
        }
  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            skeletonHealth = Mathf.Max(0, skeletonHealth - 1);
            anim.SetTrigger("getHit");
            Destroy(other.gameObject);
            if (skeletonHealth == 0)
            {            
                Destroy(gameObject);
                Instantiate(skeletonDeathPrefab,
                gameObject.transform.position, Quaternion.identity);
                Instantiate(coinPrefab,
                gameObject.transform.position, Quaternion.identity);

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
