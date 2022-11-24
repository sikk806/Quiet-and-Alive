using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBone25 : MonoBehaviour
{
    Vector3 startSpot;
    Vector3 movingSpot;
    Vector3 spot1 = new Vector3(303.49f, -193.27f, 4.0f);
    Vector3 spot2 = new Vector3(311.99f, -914.07f, 4.0f);
    bool go = true;

    Drown Drown;
    // Start is called before the first frame update
    void Start()
    {
        Drown = GameObject.Find("Body_Blood").GetComponent<Drown>();
    }

    void Update()
    {
        movingSpot = transform.position;
        if(go)
        {
            transform.position = Vector3.MoveTowards(transform.position, spot1, 0.5f);
            if (movingSpot == spot1)
            {
                go = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spot2, 0.5f);
            if (movingSpot == spot2)
            {
                go = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Drown.onBone = true;
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Drown.onBone = false;
            other.transform.SetParent(null);
        }
    }
}
