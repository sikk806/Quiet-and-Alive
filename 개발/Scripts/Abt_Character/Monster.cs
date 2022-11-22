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

    SpriteRenderer spriteRenderer;
    HoleGlitter Hole;
    Character GameCharacter;
    Animator anim;
    AudioSource Aud;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        HP = GameObject.Find("HP").GetComponent<HP>();
        Hole = GameObject.Find("2F_HOLE").GetComponent<HoleGlitter>();
        Aud = GetComponent<AudioSource>();
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

        if(playerIsClose && talked)
        {
            anim.SetBool("Close", true);
        }

        if(chasing)
        {
            if (transform.position.x < GameCharacter.transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            distance = Vector2.Distance(transform.position, GameCharacter.transform.position);
            Vector2 direction = GameCharacter.transform.position - transform.position;
            direction.Normalize();

            Vector2 fixedY = new Vector2(GameCharacter.transform.position.x, this.transform.position.y);

            transform.position = Vector2.MoveTowards(this.transform.position, fixedY, speed * Time.deltaTime);

            if (playerIsClose || check)
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
                talked = false;
                GameCharacter.stop = true;
                Aud.Play();
                Invoke("waitChangeStance", 2.3f);
            }
        }
    }

    void waitChangeStance()
    {
        GameCharacter.stop = false;
        Hole.hle = true;
        chasing = true;
    }
}
