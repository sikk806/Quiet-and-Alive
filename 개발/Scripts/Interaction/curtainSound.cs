using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class curtainSound : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;

    private int index;
    private bool waitSecond = false;

    public float wordSpeed;
    public bool playerIsClose;

    Character GameCharacter;
    AudioSource Aud;

    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        Aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && playerIsClose && !GameCharacter.talking)
        {
            StartCoroutine(Sound());
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
        Aud.Play();
        yield return new WaitForSeconds(1.0f);
        Aud.Stop();
    }
}
