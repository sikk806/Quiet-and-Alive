using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkDoor : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject ForDestroy;
    public Text dialogueText;
    public string[] dialogue;
    public Rigidbody2D rigid;

    private int index;
    private bool waitSecond = false;
    private bool goDown = false;

    public float wordSpeed;
    public bool playerIsClose;

    Character GameCharacter;

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        rigid = GameObject.Find("Character").GetComponent<Rigidbody2D>();

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
    }

    IEnumerator Typing()
    {
        GameCharacter.anim.SetBool("isSurprise", false);
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
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
            Debug.Log("check");
            GameCharacter.talking = true;
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Exit");
            GameCharacter.talking = false;


            ForDestroy = GameObject.Find("checkDoor");

            Destroy(ForDestroy);
        }
    }
}
