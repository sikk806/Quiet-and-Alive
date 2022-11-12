using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector2 startSpot;
    Vector2 movingSpot;
    Vector2 spot1;

    bool go = true;
    // Start is called before the first frame update
    void Start()
    {
        startSpot = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movingSpot = transform.position;
        spot1 = new Vector2(370.0f, -144.0f);
        if (go)
        {
            transform.position = Vector2.MoveTowards(transform.position, spot1, 0.02f);
            if (movingSpot == spot1)
            {
                go = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startSpot, 0.02f);
            if (movingSpot == startSpot)
            {
                go = true;
            }
        }
    }
}
