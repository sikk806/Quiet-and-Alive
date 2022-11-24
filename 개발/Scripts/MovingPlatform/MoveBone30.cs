using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBone30 : MonoBehaviour
{
    Vector3 startSpot;
    Vector3 movingSpot;
    Vector3 spot1 = new Vector3(347.26f, -190.21f, 4.0f);
    Vector3 spot2 = new Vector3(353.77f, -181.35f, 4.0f);
    Vector3 middleSpot = new Vector3(352.89f, -188.27f, 4.0f);
    bool go = true;
    int spot = 0;

    Drown Drown;
    // Start is called before the first frame update
    void Start()
    {
        Drown = GameObject.Find("Body_Blood").GetComponent<Drown>();
    }

    void Update()
    {
        movingSpot = transform.position;
        if (go)
        {
            if (spot == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, middleSpot, 0.5f);
                if (movingSpot == middleSpot)
                {
                    spot = 1;
                }
            }
            else if (spot == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, spot1, 0.5f);
                if (movingSpot == spot1)
                {
                    go = false;
                    spot = 0;
                }
            }
        }
        else
        {
            if (spot == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, middleSpot, 0.5f);
                if (movingSpot == middleSpot)
                {
                    spot = 1;
                }
            }
            else if (spot == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, spot2, 0.5f);
                if (movingSpot == spot2)
                {
                    go = true;
                    spot = 0;
                }
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
