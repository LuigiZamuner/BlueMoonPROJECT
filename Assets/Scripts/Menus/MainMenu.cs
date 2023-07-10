using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens for the OnClick events for the main menu buttons
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Handles the on click event from the play button
    /// </summary>

    private void Start()
    {
        LevelChanger.instance.windowCanvasGroup.alpha = 1.0f;
        StartCoroutine(LevelChanger.instance.MenuDisplay(false));
        AudioManager.PlayLoop(AudioClipName.MainMenuMusic, 0.3f);
    }

    public void HandlePlayButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick, 1);
        MenuManager.GoToMenu(MenuName.Difficulty);
	}

	/// <summary>
	/// Handles the on click event from the high score button
	/// </summary>
	public void HandleHighScoreButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick, 1);
        MenuManager.GoToMenu(MenuName.HighScore);
    }

	/// <summary>
	/// Handles the on click event from the quit button
	/// </summary>
	public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick, 1);
        Application.Quit();
    }
} 
