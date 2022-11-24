using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForNoSound : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;

    private int index;
    private bool waitSecond = false;

    public float wordSpeed;
    public bool playerIsClose;

    Character GameCharacter;

    [Header("±ôºýÀÌ´Â ¼Óµµ(0~1)")]
    public float blinkSpeed;

    private bool checkTime = false;
    private bool dark = true;
    private float timeCheck;

    public GameObject Blink;
    SpriteRenderer BlinkSr;
    Color BlinkColor;

    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        BlinkSr = Blink.GetComponent<SpriteRenderer>();
        BlinkColor = BlinkSr.color;
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
                if (BlinkColor.a < 0.8f && !dark)
                {
                    BlinkColor.a += blinkSpeed;
                    BlinkSr.color = BlinkColor;
                }
                else if (BlinkColor.a >= 0.8f && !dark)
                {
                    BlinkColor.a = 0.8f;
                    BlinkSr.color = BlinkColor;
                    dark = true;
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && playerIsClose && !GameCharacter.talking)
        {
            GameCharacter.talking = true;
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());

            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && dialogueText.text == dialogue[index] && GameCharacter.talking && !waitSecond)
        {
            NextLine();
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            waitSecond = true;
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed / dialogue[index].Length);
            waitSecond = false;
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            GameCharacter.talking = false;
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            Blink.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Blink.SetActive(false);
            playerIsClose = false;
            GameCharacter.talking = false;
            zeroText();
        }
    }
}
