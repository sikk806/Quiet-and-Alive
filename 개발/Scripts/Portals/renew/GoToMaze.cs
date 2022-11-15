using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMaze : MonoBehaviour
{
    private bool playerIsClose;
    public Character player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsClose)
        {
            player.transform.position = new Vector2(297.0f, -230.0f);
            playerIsClose = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    
}
