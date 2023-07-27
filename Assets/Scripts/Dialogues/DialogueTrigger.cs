using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool isTriggerPlayer;
    [SerializeField] 
    private GameObject visualCue;
    [SerializeField]
    private TextAsset inkJSON;
    // Start is called before the first frame update
    void Start()
    {
        isTriggerPlayer = false;
        visualCue.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggerPlayer = true;
            if (!DialogueManager.instance.dialogueIsPlaying)
            {
                visualCue.SetActive(true);
                StartCoroutine(CheckInput());
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            visualCue.SetActive(false);
            isTriggerPlayer = false;
            DialogueManager.instance.ExitDialogue();
        }
    }
    private IEnumerator CheckInput()
    {
        while (isTriggerPlayer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                visualCue.SetActive(false);
                DialogueManager.instance.EnterDialogue(inkJSON);     
                yield break;
            }
            yield return null;
        }
    }
}
