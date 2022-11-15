using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPuddle : MonoBehaviour
{
    HP HP;

    bool healing = false;
    bool checkTime = false;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        HP = GameObject.Find("HP").GetComponent<HP>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healing)
        {
            if (!checkTime)
            {
                startTime = Time.time;
                checkTime = true;
            }
            else
            {
                if (Time.time - startTime >= 1.0f)
                {
                    if (HP.nowHp < 100)
                    {
                        HP.nowHp += 10;
                    }
                    else
                    {
                        HP.nowHp = 100;
                    }
                    checkTime = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            healing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            healing = false;
        }
    }
}
