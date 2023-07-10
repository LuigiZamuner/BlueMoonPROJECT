using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : IntEventInvoker
{
    private void Start()
    {
        unityIntEvents.Add(EventName.CoinsAddedEvent, new CoinsAddedEvent());
        EventManager.AddIntInvoker(EventName.CoinsAddedEvent, this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            unityIntEvents[EventName.CoinsAddedEvent].Invoke(1);
            Destroy(gameObject);
        }
    }
}
