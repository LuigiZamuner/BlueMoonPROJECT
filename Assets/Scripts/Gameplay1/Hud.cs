using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
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

    public Image newHeart;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int health;
    public int numberOfHearts;
    public List<Image> hearts = new List<Image>();


    void Start()
    {
        EventManager.AddIntListener(EventName.CoinsAddedEvent, HandleCoinsAddedEvent);
        EventManager.AddIntListener(EventName.AddHeartEvent, AddHeart);
        // initialize score text
        coinsText.text = "Souls: " + coins;
        EventManager.AddIntListener(EventName.HealthChangedEvent, HandleHealthChangedEvent);
    }
    private void Update()
    {
        coinsText.text = "Souls: " + coins;
    }

    public int Coins
    {
        get { return coins; }
    }



    private void HandleCoinsAddedEvent(int points)
    {
        coins += points;
        coinsText.text = "Souls: " + coins;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    void HandleHealthChangedEvent(int value)
    {

        health = value;
        if(health > numberOfHearts)
        {
            health = numberOfHearts;
        }
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
       

    }
    public void AddHeart(int heartsNumber)
    {
        for (int i = 0; i < heartsNumber; i++)
        {
            numberOfHearts += heartsNumber;
            hearts.Add(Instantiate(newHeart, new Vector3(hearts[3].gameObject.transform.position.x + 43f, gameObject.transform.position.y + 226f,
            gameObject.transform.position.z), Quaternion.identity, GameObject.FindObjectOfType<Hud>().transform));
        }

    } 
    }

