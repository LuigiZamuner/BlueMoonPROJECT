using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInvoker : MonoBehaviour
{
    protected Dictionary<EventName, UnityEvent> unityEvents =
    new Dictionary<EventName, UnityEvent>();

    /// <summary>
    /// Adds the given listener for the given event name
    /// </summary>
    /// <param name="eventName">event name</param>
    /// <param name="listener">listener</param>
    public void AddListener(EventName eventName, UnityAction listener)
    {
        // only add listeners for supported events
        if (unityEvents.ContainsKey(eventName))
        {
            unityEvents[eventName].AddListener(listener);
        }
    }
}
