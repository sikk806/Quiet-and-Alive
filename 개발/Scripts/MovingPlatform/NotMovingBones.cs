using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotMovingBones : MonoBehaviour
{
    Drown Drown;
    // Start is called before the first frame update
    void Start()
    {
        Drown = GameObject.Find("Body_Blood").GetComponent<Drown>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Drown.onBone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Drown.onBone = false;
        }
    }
}
