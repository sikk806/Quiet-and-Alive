using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    //private Color BlinkColor;
    public bool blink;
    private bool alphaUp = false;
    private float newAlpha;
    [Range(0, 10)]
    public float speed = 1;

    public SpriteRenderer BlinkRenderer;


    private void Start()
    {
        BlinkRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (blink)
        {
            Debug.Log("check if blink");
            gameObject.SetActive(true);
            if (alphaUp)
            {
                if ((BlinkRenderer.color.a) >= 1.0f)
                {
                    alphaUp = false;
                }
                newAlpha = BlinkRenderer.color.a + (0.001f * speed);
                BlinkRenderer.color = new Color(1.0f, 1.0f, 1.0f, newAlpha);
            }
            else
            {
                if ((BlinkRenderer.color.a) <= 0.0f)
                {
                    alphaUp = true;
                }
                newAlpha = BlinkRenderer.color.a - (0.001f * speed);
                BlinkRenderer.color = new Color(1.0f, 1.0f, 1.0f, newAlpha);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("check on trigger enter");
        if (other.CompareTag("Player"))
        {
            blink = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("check on trigger Exit");
        if (other.CompareTag("Player"))
        {
            blink = false;
        }
    }
}
