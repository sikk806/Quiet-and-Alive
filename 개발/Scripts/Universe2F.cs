using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe2F : MonoBehaviour
{
    Character GameCharacter;

    private float X, Y;

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        X = GameCharacter.transform.position.x;
        if(X > 21.3f)
        {
            X = 21.3f;
        }
        else if(X < -9.0f)
        {
            X = -9.0f;
        }

        transform.position = new Vector3(X*0.3f, -4.0f, 10);
    }
}
