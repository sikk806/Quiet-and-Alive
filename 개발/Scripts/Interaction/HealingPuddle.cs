using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPuddle : MonoBehaviour
{
    HP HP;

    bool healing = false;
    bool checkTime = false;
    bool alphaTime = false;

    float startAlphaTime;
    float startTime;

    public GameObject healPuddle;
    Color healAlpha;
    SpriteRenderer healRenderer;



    // Start is called before the first frame update
    void Start()
    {
        HP = GameObject.Find("HP").GetComponent<HP>();
        healRenderer = healPuddle.GetComponent<SpriteRenderer>();
        healAlpha = healRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (healing)
        {
            if (!alphaTime)
            {
                startAlphaTime = Time.time;
                alphaTime = true;
            }
            else
            {
                if (Time.time - startAlphaTime >= 0.1f)
                {
                    healAlpha = new Color(1, 1, 1, healAlpha.a + 0.05f);
                    healRenderer.color = healAlpha;
                    alphaTime = true;
                }
                if(healAlpha.a >= 0.8f)
                {
                    healAlpha.a = 0.8f;
                }
            }
             
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
        else
        {
            if(!alphaTime)
            {
                startAlphaTime = Time.time;
                alphaTime = true;
            }
            else
            {
                if (Time.time - startAlphaTime >= 0.1f)
                {
                    healAlpha = new Color(1, 1, 1, healAlpha.a - 0.05f);
                    healRenderer.color = healAlpha;
                    alphaTime = true;
                }
                if (healAlpha.a <= 0.0f)
                {
                    healAlpha.a = 0.0f;
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
