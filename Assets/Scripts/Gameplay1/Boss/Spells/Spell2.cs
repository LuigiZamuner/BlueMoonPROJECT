using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell2 : IntEventInvoker
{
    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    private int damage = 1;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        unityIntEvents.Add(EventName.TakeDamageEvent, new TakeDamageEvent());
        EventManager.AddIntInvoker(EventName.TakeDamageEvent, this);
        StartCoroutine(WaitForSecondsToDestroy(4));

        rb2D.AddForce(Vector2.right * 22, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DamagePlayer();
            Destroy(gameObject);
        }

    }
    private void DamagePlayer()
    {
            unityIntEvents[EventName.TakeDamageEvent].Invoke(damage);
    }
    private IEnumerator WaitForSecondsToDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
        
    }
}
