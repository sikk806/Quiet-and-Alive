using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleGlitter : MonoBehaviour
{
    public GameObject Hole;
    public GameObject Monster;
    public bool hle = false;

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
        if(other.CompareTag("Player") && hle)
        {
            Hole.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Hole.SetActive(false);
        }
    }
}
