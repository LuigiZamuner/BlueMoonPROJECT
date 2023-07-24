using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Listens for the OnClick events for the difficulty menu buttons
/// </summary>
public class DifficultyMenu : IntEventInvoker
{
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>

    void Start()
    {
        // add event component and add invoker to event manager
        unityIntEvents.Add(EventName.GameStartedEvent, new GameStartedEvent());
        EventManager.AddIntInvoker(EventName.GameStartedEvent, this);
    }

    /// <summary>
    /// Handles the on click event from the easy button
    /// </summary>
    public void HandleMediumButtonOnClickEvent()
    {
        StartCoroutine(LevelChanger.instance.SceneChanger("Gameplay1"));
        AudioManager.Play(AudioClipName.MenuButtonClick, 1);
		AudioManager.Stop(AudioClipName.MainMenuMusic);
	}

	/// <summary>
	/// Handles the on click event from the medium button
	/// </summary>
	public void HandleHardButtonOnClickEvent()
    {
        StartCoroutine(LevelChanger.instance.SceneChanger("Gameplay1"));
        AudioManager.Play(AudioClipName.MenuButtonClick, 1);
        AudioManager.Stop(AudioClipName.MainMenuMusic);
    }

}
