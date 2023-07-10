using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class DoubleJump : IntEventInvoker
{
    private void Start()
    {
        unityIntEvents.Add(EventName.GetDoubleJumpEvent, new GetDoubleJumpEvent());
        EventManager.AddIntInvoker(EventName.GetDoubleJumpEvent, this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            unityIntEvents[EventName.GetDoubleJumpEvent].Invoke(1);
        }
    }
}
