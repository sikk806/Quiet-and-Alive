using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterPortal : MonoBehaviour
{
    public bool playerIsClose = false;
    bool stop = false;

    float x = 0.0f;
    float y = 0.0f;

    public GameObject Portal;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsClose)
        {
            Portal.SetActive(true);
            if (!stop)
            {
                x += 0.005f;
                y += 0.005f;
                if (x >= 1.0f)
                {
                    stop = true;
                }
                Portal.transform.localScale = new Vector3(x, y, 1.0f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
