using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyingEnemy : IntEventInvoker
{
    [SerializeField]
    GameObject coinPrefab;
    [SerializeField]
    GameObject flyingEnemyDeathPref;

    FieldOfView fieldOfView;
    Follow follow;
    private Animator anim;
    private int flyingEnemyHealth = 5;
    private int damage = 1;


    private void Start()
    {
        anim = GetComponent<Animator>();
        follow = GetComponent<Follow>();
        fieldOfView = GetComponent<FieldOfView>();
        unityIntEvents.Add(EventName.TakeDamageEvent, new TakeDamageEvent());
        EventManager.AddIntInvoker(EventName.TakeDamageEvent, this);
    }
    private void Update()
    {
        if (fieldOfView.FieldOfViewEnemy())
        {
            follow.SetImpulseForce(8);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            flyingEnemyHealth = Mathf.Max(0, flyingEnemyHealth - 1);
            anim.SetTrigger("getHit");
            Destroy(other.gameObject);
            if (flyingEnemyHealth == 0)
            {
                Destroy(gameObject);

                Instantiate(coinPrefab,
                gameObject.transform.position, Quaternion.identity);

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            unityIntEvents[EventName.TakeDamageEvent].Invoke(damage);
        }   
    }
}

