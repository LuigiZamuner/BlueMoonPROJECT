using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI lostCoins;
    int losts;
    Save save;
    Hud hud;
    void Start()
    {
        hud = GameObject.FindObjectOfType<Hud>();
        save = GetComponent<Save>();
        string[] data = PlayerPrefs.GetString("Save").Split("|");
        losts = hud.coins - int.Parse(data[0]);
        Cursor.visible = true;
        lostCoins.text = "Voce perdeu " + losts + " alma(s)";
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
