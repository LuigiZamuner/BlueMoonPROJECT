using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOpen = false;
    public static PauseGame instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // check for pausing game
        if (Input.GetKeyDown("escape") && !isOpen)
        {
            isOpen = true;
            MenuManager.GoToMenu(MenuName.Pause);
            AudioManager.Play(AudioClipName.PauseGame, 1);

        }
    }
}
