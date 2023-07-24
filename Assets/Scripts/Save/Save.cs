using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    Ghost ghost;
    Hud hud;
    private void Start()
    {
        ghost = GameObject.FindObjectOfType<Ghost>();
        hud = GameObject.FindObjectOfType<Hud>();
    }

    public void SaveGame()
    {
        string save = "";
        save += hud.coins.ToString()+ "|";
        save += ghost.pegouDoubleJump.ToString() + "|";
        save += ghost.position.ToString() + "|";
        save += ghost.health.ToString() + "|";


        PlayerPrefs.SetString("Save", save);
        Debug.Log("JogoSalvo");
    }
    public void LoadSave()
    {
        string[] data = PlayerPrefs.GetString("Save").Split("|");
        ghost = GameObject.FindObjectOfType<Ghost>();
        hud = GameObject.FindObjectOfType<Hud>();
        hud.coins = int.Parse(data[0]);
        ghost.pegouDoubleJump = int.Parse(data[1]);
        if (int.Parse(data[1]) == 0 )
        {
            Spawns.instance.SpawnDoubleJumpObject();
        }
        ghost.transform.position= ParseVector3(data[2]);
        ghost.health = int.Parse(data[3]);
        Spawns.instance.DestroyTheEnemys();
        Spawns.instance.SpawnTheEnemys();

    }
    private Vector3 ParseVector3(string vector3String)
    {
        string[] values = vector3String.Trim('(', ')').Split(',');

        float x = float.Parse(values[0]);
        float y = float.Parse(values[1]);
        float z = float.Parse(values[2]);

        return new Vector3(x, y, z);
    }
}
