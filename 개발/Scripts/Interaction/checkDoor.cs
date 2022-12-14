using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkDoor : MonoBehaviour
{
    public GameObject DestroyObject;
    public GameObject dialoguePanel;
    public GameObject ForDestroy;
    public Text dialogueText;
    public string[] dialogue;
    public Rigidbody2D rigid;

    private int index;
    private bool waitSecond = false;

    public float wordSpeed;
    public bool playerIsClose;

    Character GameCharacter;
    AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        rigid = GameObject.Find("Character").GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
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
        Destroy(ForDestroy);
    }

    IEnumerator Typing()
    {
        GameCharacter.anim.SetBool("isSurprise", false);
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
            if (index == 2)
            {
                StartCoroutine(Typing());
            }
            else
            {

                StartCoroutine(Typing());
            }
        }
        else
        {
            GameCharacter.talking = false;
            GameCharacter.anim.SetBool("isSurprise", false);
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameCharacter.talking = true;
            dialoguePanel.SetActive(true);
            AudioSource.Play();
            StartCoroutine(Typing());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameCharacter.talking = false;
            ForDestroy = GameObject.Find("checkDoor");
        }
    }
}
