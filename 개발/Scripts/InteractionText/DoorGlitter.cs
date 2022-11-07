using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGlitter : MonoBehaviour
{
    public GameObject DoorLight;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorLight.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoorLight.SetActive(false);
        }
    }
}
