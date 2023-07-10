using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawns : MonoBehaviour
{
    [SerializeField]
    GameObject doubleJumpPrefab;
    [SerializeField]
    GameObject skeletonPrefab;
    [SerializeField]
    GameObject flyingEnemyPrefab;
    [SerializeField]
    private List<GameObject> listaSpawn = new List<GameObject>();
    List<GameObject> enemys = new List<GameObject>();

    public static Spawns instance;
    void Start()
    {
        instance = this;
        SpawnTheEnemys();
        SpawnDoubleJumpObject();
    }
    public void SpawnTheEnemys()
    {
        enemys.Add(Instantiate(skeletonPrefab, listaSpawn[0].transform.position, Quaternion.identity));
        enemys.Add(Instantiate(skeletonPrefab, listaSpawn[1].transform.position, Quaternion.identity));
        enemys.Add(Instantiate(skeletonPrefab, listaSpawn[2].transform.position, Quaternion.identity));
        enemys.Add(Instantiate(skeletonPrefab, listaSpawn[3].transform.position, Quaternion.identity));
        enemys.Add(Instantiate(flyingEnemyPrefab, listaSpawn[4].transform.position, Quaternion.identity));
    }
    public void DestroyTheEnemys()
    {
        for(int i = 0; i< enemys.Count; i++)
        {
            Destroy(enemys[i]);
        }
    }
    public void SpawnDoubleJumpObject()
    {
        Instantiate(doubleJumpPrefab, listaSpawn[5].transform.position, Quaternion.identity);
    }
}
