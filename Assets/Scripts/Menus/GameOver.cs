using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    Save save;
    void Start()
    {
        save = GetComponent<Save>();
        Cursor.visible = true;
        // pause the game when added to the scene
        Time.timeScale = 0;
    }
    public void HandleRetryButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick, 1);

        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        if(PlayerPrefs.GetString("Save") == "") 
        {
            MenuManager.GoToMenu(MenuName.Gameplay1);
        }
        else
        {
            save.LoadSave();
        }
        
    }
    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick, 1);

        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }

}
