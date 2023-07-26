using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Retrieves and displays high score and listens for
/// the OnClick events for the high score menu button
/// </summary>
public class HighScoreMenu : MonoBehaviour
{
    [SerializeField]
    GameObject message;
    [SerializeField]
    GameObject buttonObject;

    Save save;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        buttonObject.SetActive(false);
        message.SetActive(false);
        save = GetComponent<Save>();
        if (PlayerPrefs.GetString("Save") == "")
        {
            message.SetActive(true);
        }
        else
        {
            buttonObject.SetActive(true);
        }

    }

    public void HandleLoadButtonOnClickEvent()
    {
        StartCoroutine(CarregarCenaDoJogo());
        AudioManager.Play(AudioClipName.MenuButtonClick, 1);
        AudioManager.Stop(AudioClipName.MainMenuMusic);
        Time.timeScale = 1;


    }

    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick, 1);

        // unpause game and go to main menu
        Time.timeScale = 1;
        MenuManager.GoToMenu(MenuName.Main);
    }

    IEnumerator CarregarCenaDoJogo()
    {
        AsyncOperation cenaAssincrona = SceneManager.LoadSceneAsync("Gameplay1");
        cenaAssincrona.allowSceneActivation = false;

        cenaAssincrona.completed += CenaCarregada;

        while (!cenaAssincrona.isDone)
        {
            if (cenaAssincrona.progress >= 0.9f)
            {
                cenaAssincrona.allowSceneActivation = true;
            }

            yield return null;
        }

    }

    void CenaCarregada(AsyncOperation cenaAssicrona)
    {
        save.LoadSave();
        Spawns.instance.DestroyTheEnemys();
        Debug.Log("Carregado com sucesso");
    }

}
