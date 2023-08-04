using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondForm : MonoBehaviour
{
    [SerializeField]
    GameObject Spell1Prefab;
    [SerializeField]
    GameObject Spell2Prefab;
    private int[] attackXValues = { 13, 17, 21, 26, 30};
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 12; i ++)
        {
            int waitTime = 3 + 3 * i;
            StartCoroutine(WaitForSecondsToInstatiate(waitTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitForSecondsToInstatiate(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        int randomNumber = Random.Range(-6,6);
        int randomXIndex = Random.Range(0, attackXValues.Length);
        int randomXIndex2 = Random.Range(0, attackXValues.Length);


        if(seconds > 21)
        {
            Instantiate(Spell1Prefab, new Vector3(gameObject.transform.position.x - attackXValues[randomXIndex], gameObject.transform.position.y - 6), Quaternion.identity);
            Instantiate(Spell1Prefab, new Vector3(gameObject.transform.position.x + attackXValues[randomXIndex2], gameObject.transform.position.y - 6), Quaternion.identity);
            Instantiate(Spell1Prefab, new Vector3(gameObject.transform.position.x + randomNumber, gameObject.transform.position.y - 6), Quaternion.identity);
            Instantiate(Spell2Prefab, new Vector3(gameObject.transform.position.x - 30, gameObject.transform.position.y - 15), Quaternion.identity);
            Instantiate(Spell2Prefab, new Vector3(gameObject.transform.position.x - 30, gameObject.transform.position.y -7), Quaternion.identity);
            Instantiate(Spell2Prefab, new Vector3(gameObject.transform.position.x - 30, gameObject.transform.position.y - 1), Quaternion.identity);
        }
        else
        {
            Instantiate(Spell1Prefab, new Vector3(gameObject.transform.position.x - attackXValues[randomXIndex], gameObject.transform.position.y - 6), Quaternion.identity);
            Instantiate(Spell1Prefab, new Vector3(gameObject.transform.position.x + randomNumber, gameObject.transform.position.y - 6), Quaternion.identity);
            Instantiate(Spell1Prefab, new Vector3(gameObject.transform.position.x + attackXValues[randomXIndex2], gameObject.transform.position.y - 6), Quaternion.identity);
        }
    }
}
