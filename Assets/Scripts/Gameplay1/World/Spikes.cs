using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spikes : IntEventInvoker
{
    private void Start()
    {
        unityIntEvents.Add(EventName.TakeDamageEvent, new TakeDamageEvent());
        EventManager.AddIntInvoker(EventName.TakeDamageEvent, this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            unityIntEvents[EventName.TakeDamageEvent].Invoke(6);
        }
    }
}
