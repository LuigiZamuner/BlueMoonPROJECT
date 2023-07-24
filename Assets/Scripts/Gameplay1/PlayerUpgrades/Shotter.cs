using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotter : IntEventInvoker
{
    private void Start()
    {
        unityIntEvents.Add(EventName.GetShotterEvent, new GetShotterEvent());
        EventManager.AddIntInvoker(EventName.GetShotterEvent, this);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            unityIntEvents[EventName.GetShotterEvent].Invoke(1);
        }
    }
}
