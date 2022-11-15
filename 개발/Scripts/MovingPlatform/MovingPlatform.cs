using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector2 startSpot;
    Vector2 movingSpot;
    Vector2 spot1 = new Vector2(370.0f, -144.0f);
    Vector2 spot2 = new Vector2(366.0f, -151.0f);
    Vector2 spot3 = new Vector2(361.0f, -160.0f);

    Drown Drown;

    bool go = true;
    int step = 0;
    // Start is called before the first frame update
    void Start()
    {
        startSpot = transform.position;
        Drown = GameObject.Find("Body_Blood").GetComponent<Drown>();
    }

    // Update is called once per frame
    void Update()
    {
        movingSpot = transform.position;
        if (go)
        {
            if (step == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, spot1, 0.02f);
                if(movingSpot == spot1)
                {
                    step = 1;
                }
            }
            else if(step == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, spot2, 0.02f);
                if (movingSpot == spot2)
                {
                    step = 2;
                }
            }
            else if(step == 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, spot3, 0.02f);
                if (movingSpot == spot3)
                {
                    step = 0;
                    go = false;
                }
            }
        }
        else
        {
            if (step == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, spot2, 0.02f);
                if (movingSpot == spot2)
                {
                    step = 1;
                }
            }
            else if (step == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, spot1, 0.02f);
                if (movingSpot == spot1)
                {
                    step = 2;
                }
            }
            else if (step == 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, startSpot, 0.02f);
                if (movingSpot == startSpot)
                {
                    step = 0;
                    go = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
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
