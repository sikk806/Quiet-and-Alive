using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lib1DoorCheck : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    [Header ("?????̴? ?ӵ?(0~1)")]
    public float blinkSpeed;
    public bool playerIsClose;
    private bool waitSecond = false;
    private bool checkTime = false;
    private bool dark = true;
    private float timeCheck;

    public GameObject Blink;
    SpriteRenderer BlinkSr;
    Color BlinkColor;

    Character GameCharacter;
    AudioSource AudioSource;

    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        AudioSource = GetComponent<AudioSource>();
        BlinkSr = Blink.GetComponent<SpriteRenderer>();
        BlinkColor = BlinkSr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(!checkTime)
        {
            timeCheck = Time.time;
            checkTime = true;
        }
        else
        {
            if(Time.time - timeCheck >= 0.1)
            {
                if(BlinkColor.a > 0.0f && dark)
                {
                    BlinkColor.a -= blinkSpeed;
                    BlinkSr.color = BlinkColor;
                }
                else if(BlinkColor.a <= 0.0f && dark)
                {
                    BlinkColor.a = 0.0f;
                    BlinkSr.color = BlinkColor;
                    dark = false;
                }
                if(BlinkColor.a < 0.8f && !dark)
                {
                    BlinkColor.a += blinkSpeed;
                    BlinkSr.color = BlinkColor;
                }
                else if(BlinkColor.a >= 0.8f && !dark)
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
            StartCoroutine(Sound());
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

        if (Input.GetKeyDown(KeyCode.Z) && dialogueText.text == dialogue[index] && GameCharacter.talking &&! waitSecond)
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            GameCharacter.talking = false;
            zeroText();
        }
    }

    public IEnumerator Sound()
    {
        AudioSource.Play();
        yield return new WaitForSeconds(1.0f);
        AudioSource.Play();
    }
}
