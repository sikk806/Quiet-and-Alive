using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder_2 : MonoBehaviour
{
    public GameObject LadderIN;
    public bool playerIsClose = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            LadderIN.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            LadderIN.SetActive(false);
        }
    }
}
