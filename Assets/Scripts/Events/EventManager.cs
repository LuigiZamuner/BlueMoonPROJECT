﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages connections between event listeners and event invokers
/// </summary>
public static class EventManager
{
	#region Fields

	static Dictionary<EventName, List<IntEventInvoker>> intInvokers = 
		new Dictionary<EventName, List<IntEventInvoker>>();
	static Dictionary<EventName, List<UnityAction<int>>> intListeners =
		new Dictionary<EventName, List<UnityAction<int>>>();

    static Dictionary<EventName, List<EventInvoker>> invokers =
		new Dictionary<EventName, List<EventInvoker>>();
    static Dictionary<EventName, List<UnityAction>> listeners =
		new Dictionary<EventName, List<UnityAction>>();

    #endregion

    #region Public methods

    /// <summary>
    /// Initializes the event manager
    /// </summary>
    public static void Initialize()
    {
		// create empty lists for all the dictionary entries
		foreach (EventName name in Enum.GetValues(typeof(EventName)))
        {
			if (!intInvokers.ContainsKey(name))
            {
				intInvokers.Add(name, new List<IntEventInvoker>());
				intListeners.Add(name, new List<UnityAction<int>>());
			}
            else
            {
				intInvokers[name].Clear();
				intListeners[name].Clear();
			}
		}
	}
		
	/// <summary>
	/// Adds the given invoker for the given event name
	/// </summary>
	/// <param name="eventName">event name</param>
	/// <param name="invoker">invoker</param>
	public static void AddIntInvoker(EventName eventName, IntEventInvoker invoker)
    {
		// add listeners to new invoker and add new invoker to dictionary
		foreach (UnityAction<int> listener in intListeners[eventName])
        {
			invoker.AddListener(eventName, listener);
		}
		intInvokers[eventName].Add(invoker);
	}

	/// <summary>
	/// Adds the given listener for the given event name
	/// </summary>
	/// <param name="eventName">event name</param>
	/// <param name="listener">listener</param>
	public static void AddIntListener(EventName eventName, UnityAction<int> listener)
    {
		// add as listener to all invokers and add new listener to dictionary
		foreach (IntEventInvoker invoker in intInvokers[eventName])
        {
			invoker.AddListener(eventName, listener);
		}
		intListeners[eventName].Add(listener);
	}

    public static void AddInvoker(EventName eventName, EventInvoker invoker)
    {
        // add listeners to new invoker and add new invoker to dictionary
        foreach (UnityAction listener in listeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        invokers[eventName].Add(invoker);
    }

    public static void AddListener(EventName eventName, UnityAction listener)
    {
        // add as listener to all invokers and add new listener to dictionary
        foreach (EventInvoker invoker in invokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        listeners[eventName].Add(listener);
    }
    /// <summary>
    /// Removes the given invoker for the given event name
    /// </summary>
    /// <param name="eventName">event name</param>
    /// <param name="invoker">invoker</param>
    /// 

    public static void RemoveInvoker(EventName eventName, EventInvoker invoker)
    {
        // remove invoker from dictionary
        invokers[eventName].Remove(invoker);
    }
    public static void RemoveIntInvoker(EventName eventName, IntEventInvoker invoker)
    {
		// remove invoker from dictionary
		intInvokers[eventName].Remove(invoker);
	}



    #endregion
}
