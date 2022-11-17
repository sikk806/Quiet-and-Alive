using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_InHeart : MonoBehaviour
{
    bool playerIsClose = false;

    public Character Character;
    // Start is called before the first frame update
    void Start()
    {
        Character = GameObject.Find("Character").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsClose)
        {
            Character.transform.position = new Vector2(267.7f, -164.1f);
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
