using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrop : IntEventInvoker
{
    // Start is called before the first frame update
  

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Ghost player = other.gameObject.GetComponent<Ghost>();
            player.health += 1 ;
            Destroy(gameObject);
        }
    }
}
