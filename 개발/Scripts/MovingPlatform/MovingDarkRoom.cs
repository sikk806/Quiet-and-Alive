using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDarkRoom : MonoBehaviour
{
    public List<Vector3> spot = new List<Vector3>();

    public float speed;
    bool go = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movingSpot = transform.position;
        if (go)
        {
            transform.position = Vector3.MoveTowards(transform.position, spot[1], speed);
            if (movingSpot == spot[1])
            {
                go = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spot[0], speed);
            if (movingSpot == spot[0])
            {
                go = true;
            }
        }
    }
}

void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        other.transform.SetParent(transform);
    }
}

void OnTriggerExit2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        other.transform.SetParent(null);
    }
}
