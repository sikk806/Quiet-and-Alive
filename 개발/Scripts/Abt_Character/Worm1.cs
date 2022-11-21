using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worm1 : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject Worm;
    public Text dialogueText;
    public string[] dialogue;

    private Collider2D WormCollider;
    private int index;
    private bool waitSecond = false;

    public float wordSpeed;

    bool playerIsClose = false;

    Character GameCharacter;
    ThrowBooks throwBooks;

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        //throwBooks = GameObject.Find("ThrowBooks").GetComponent<ThrowBooks>();
        WormCollider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

    IEnumerator ThrowBooks()
    {
        WormCollider.enabled = false;
        yield return new WaitForSeconds(3.0f);
        //throwBooks.endTalk = true;
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
            StartCoroutine(ThrowBooks());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
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
}
