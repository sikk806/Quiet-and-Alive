using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    private int index;
    private float checkTime;
    private bool doOnce = false;
    private bool chasing = false;
    private bool talked = false;
    private bool check = false;
    private bool timeCheck = false;
    private float distance;

    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    public float wordSpeed;
    public bool playerIsClose;
    public float speed;
    public float distanceBetween;

    private HP HP;

    Character GameCharacter;

    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        HP = GameObject.Find("HP").GetComponent<HP>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && playerIsClose && !GameCharacter.talking && !doOnce)
        {
            GameCharacter.talking = true;
            if(dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());

            }
        }

        if(Input.GetKeyDown(KeyCode.Z) && dialogueText.text == dialogue[index] && GameCharacter.talking)
        {
            NextLine();
        }

        if(chasing)
        {
            distance = Vector2.Distance(transform.position, GameCharacter.transform.position);
            Vector2 direction = GameCharacter.transform.position - transform.position;
            direction.Normalize();

            transform.position = Vector2.MoveTowards(this.transform.position, GameCharacter.transform.position, speed * Time.deltaTime);

            if(playerIsClose || check)
            {
                if (!timeCheck)
                {
                    checkTime = Time.time;
                    timeCheck = true;
                }
                check = true;
                if (Time.time - checkTime >= 1)
                {
                    HP.nowHp -= 10;
                    timeCheck = false;
                }
            }
            if (!playerIsClose)
            {
                check = false;
            }
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
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if(index < dialogue.Length -1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            GameCharacter.talking = false;
            doOnce = true;
            talked = true;
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
            zeroText();
            if (talked)
            {
                chasing = true;
            }
        }
    }
}
