using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showInterActions : MonoBehaviour
{
    [Header("±ôºýÀÌ´Â ¼Óµµ(0~1)")]
    public float blinkSpeed;

    private bool checkTime = false;
    private bool dark = true;
    private float timeCheck;

    public GameObject Blink;
    public SpriteRenderer BlinkSr;
    Color BlinkColor;
    // Start is called before the first frame update
    void Start()
    {
        BlinkColor = BlinkSr.color;
        BlinkSr = Blink.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkTime)
        {
            timeCheck = Time.time;
            checkTime = true;
        }
        else
        {
            if (Time.time - timeCheck >= 0.1)
            {
                if (BlinkColor.a > 0.0f && dark)
                {
                    BlinkColor.a -= blinkSpeed;
                    BlinkSr.color = BlinkColor;
                }
                else if (BlinkColor.a <= 0.0f && dark)
                {
                    BlinkColor.a = 0.0f;
                    BlinkSr.color = BlinkColor;
                    dark = false;
                }
                if (BlinkColor.a < 1.0f && !dark)
                {
                    BlinkColor.a += blinkSpeed;
                    BlinkSr.color = BlinkColor;
                }
                else if (BlinkColor.a >= 1.0f && !dark)
                {
                    BlinkColor.a = 1.0f;
                    BlinkSr.color = BlinkColor;
                    dark = true;
                }

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }
}
