using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    private GameObject player;
    private float X, Y;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        X = player.transform.position.x;
        Y = player.transform.position.y;

        transform.position = new Vector3(X, Y, 4);
    }
}
