using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell1 : IntEventInvoker
{
    FieldOfView fieldOfView;
    int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSecondsToDestroy(1.7f));
        fieldOfView = GetComponent<FieldOfView>();
        unityIntEvents.Add(EventName.TakeDamageEvent, new TakeDamageEvent());
        EventManager.AddIntInvoker(EventName.TakeDamageEvent, this);
    }

    // Update is called once per frame
    private void DamagePlayer()
    {
        if (fieldOfView.PlayerContact())
        {
            unityIntEvents[EventName.TakeDamageEvent].Invoke(damage);
        }
    }
    private IEnumerator WaitForSecondsToDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
