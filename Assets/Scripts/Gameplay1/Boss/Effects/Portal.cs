using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    GameObject secondPartBoss;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(WaitForSecondsToDestroy(5));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitForSecondsToDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(secondPartBoss, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 13.5f), Quaternion.identity);
        Destroy(gameObject);
    }
}
