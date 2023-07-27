using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTemple : IntEventInvoker
{
    private bool isTrigger;
    // Start is called before the first frame update
    void Start()
    {
        unityIntEvents.Add(EventName.AddHeartEvent, new AddHeartEvent());
        EventManager.AddIntInvoker(EventName.AddHeartEvent, this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
            StartCoroutine(CheckInput());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;

        }
    }
    private IEnumerator CheckInput()
    {
        while (isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                unityIntEvents[EventName.AddHeartEvent].Invoke(1);
                Destroy(gameObject);
                yield break;
            }
            yield return null;
        }
    }
}

