using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // check for pausing game
        if (Input.GetKeyDown("escape"))
        {
            MenuManager.GoToMenu(MenuName.Pause);
            AudioManager.Play(AudioClipName.PauseGame, 1);

        }
    }
}
