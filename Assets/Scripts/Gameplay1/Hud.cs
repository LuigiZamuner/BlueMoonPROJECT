using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    // Start is called before the first frame update
    // score support
    [SerializeField]
    TextMeshProUGUI coinsText;
    public int coins = 0;

    // health support
    [SerializeField]
    Slider healthBar;

    void Start()
    {
        // add listener for PointsAddedEvent
        EventManager.AddIntListener(EventName.CoinsAddedEvent, HandleCoinsAddedEvent);

        // initialize score text
        coinsText.text = "Coins: " + coins;

        // add listener for HealthChangedEvent
        EventManager.AddIntListener(EventName.HealthChangedEvent, HandleHealthChangedEvent);
    }
    private void Update()
    {
        coinsText.text = "Coins: " + coins;
    }

    public int Coins
    {
        get { return coins; }
    }



    private void HandleCoinsAddedEvent(int points)
    {
        coins += points;
        coinsText.text = "Coins: " + coins;
    }

    void HandleHealthChangedEvent(int value)
    {
        healthBar.value = value;
    }
}
