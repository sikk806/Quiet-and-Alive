using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bones : MonoBehaviour
{

    public bool playerIsClose;
    public Character player;
 

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose == true)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = new Vector2(player.transform.position.x + 5.0f, player.transform.position.y);
            }
        }
    }  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
