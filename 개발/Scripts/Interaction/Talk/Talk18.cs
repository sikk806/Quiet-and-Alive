using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk18 : MonoBehaviour
{
    public GameObject Door;
    public bool check = false;
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
    public GameObject Worm;
    Animator WormAnim;
    EP EP;

    fireFly2 FF1;
    fireFly2 FF2;
    fireFly2 FF3;
    fireFly2 FF4;
    fireFly2 FF5;
    fireFly2 FF6;
    fireFly2 FF7;
    fireFly2 FF8;
    fireFly2 FF9;
    fireFly2 FF10;
    fireFly2 FF11;

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        TriggerCollider = gameObject.GetComponent<Collider2D>();
        WormAnim = Worm.GetComponent<Animator>();
        EP = GameObject.Find("EP").GetComponent<EP>();
        FF1 = GameObject.Find("fireFly (1)").GetComponent<fireFly2>();
        FF2 = GameObject.Find("fireFly (2)").GetComponent<fireFly2>();
        FF3 = GameObject.Find("fireFly (3)").GetComponent<fireFly2>();
        FF4 = GameObject.Find("fireFly (4)").GetComponent<fireFly2>();
        FF5 = GameObject.Find("fireFly (5)").GetComponent<fireFly2>();
        FF6 = GameObject.Find("fireFly (6)").GetComponent<fireFly2>();
        FF7 = GameObject.Find("fireFly (7)").GetComponent<fireFly2>();
        FF8 = GameObject.Find("fireFly (8)").GetComponent<fireFly2>();
        FF9 = GameObject.Find("fireFly (9)").GetComponent<fireFly2>();
        FF10 = GameObject.Find("fireFly (10)").GetComponent<fireFly2>();
        FF11 = GameObject.Find("fireFly (11)").GetComponent<fireFly2>();
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
            if (index == 1)
            {
                WormAnim.SetBool("Angry", true);
                FF1.fly = true;
                FF2.fly = true;
                FF3.fly = true;
                FF4.fly = true;
                FF5.fly = true;
                FF6.fly = true;
                FF7.fly = true;
                FF8.fly = true;
                FF9.fly = true;
                FF10.fly = true;
                FF11.fly = true;
                FaceImage.sprite = Faces[1];
            }
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            Door.SetActive(true);
            EP.nowEp += 1;
            TriggerCollider.enabled = false;
            GameCharacter.talking = false;
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && check)
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && check)
        {
            TriggerCollider.enabled = false;
            playerIsClose = false;
            GameCharacter.talking = false;
            zeroText();
        }
    }
}

