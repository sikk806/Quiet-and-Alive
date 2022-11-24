using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk17 : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Image FaceImage;
    public Text dialogueText;
    public string[] dialogue;

    private Collider2D TriggerCollider;
    private int index;
    private bool waitSecond = false;

    public float wordSpeed;
    public bool playerIsClose;

    Character GameCharacter;

    public Sprite[] Faces;
    public GameObject Talk;
    public GameObject Worm;
    Animator WormAnim;
    Talk18 talk18;


    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        TriggerCollider = gameObject.GetComponent<Collider2D>();
        talk18 = Talk.GetComponent<Talk18>();
        WormAnim = Worm.GetComponent<Animator>();
    }

    void Update()
    {
        if (playerIsClose && !GameCharacter.talking)
        {
            GameCharacter.talking = true;
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                FaceImage.sprite = Faces[0];
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
            if(index == 1)
            {
                WormAnim.SetBool("back", true);
                FaceImage.sprite = Faces[1];
            }
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            talk18.check = true;
            TriggerCollider.enabled = false;
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
            TriggerCollider.enabled = false;
            playerIsClose = false;
            GameCharacter.talking = false;
            zeroText();
        }
    }
}

