using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portal_Lib1ToInBody : MonoBehaviour
{
    bool checking = false;
    float checkTime;
    private bool playerIsClose;
    private bool changeScene = false;
    private bool doOnce = false;
    private bool one = false;
    public Character player;
    public GameObject checkMonster;
    public SpriteRenderer FadeOut;
    private Collider2D BookCollider;
    
    Color FadeOutAlpha;

    [Header("깜빡이는 속도(0~1)")]
    public float blinkSpeed;

    private bool checkT = false;
    private bool dark = true;
    private float timeCheck;

    public GameObject Blink;
    SpriteRenderer BlinkSr;
    Color BlinkColor;

    public GameObject dialoguePanel;
    public Image FaceImage; 
    public Text dialogueText;
    public string[] dialogue;
    private int index;
    public Sprite[] Faces;
    Character GameCharacter;
    private bool waitSecond = false;

    public float wordSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        player = FindObjectOfType<Character>();
        BookCollider = gameObject.GetComponent<Collider2D>();
        FadeOutAlpha = FadeOut.color;
        BlinkSr = Blink.GetComponent<SpriteRenderer>();
        BlinkColor = BlinkSr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose && !GameCharacter.talking && !one)
        {
            one = true;
            FaceImage.sprite = Faces[0];
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

        if (!checkT)
        {
            timeCheck = Time.time;
            checkT = true;
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
        if (playerIsClose == true && Input.GetKeyDown(KeyCode.Z))
        {
            player.stop = true;
            changeScene = true;
        }
        if (changeScene)
        {
            if (!doOnce)
            {
                StartCoroutine(waitforsec());
            }
            if (!checking)
            {
                checkTime = Time.time;
                checking = true;
            }
            else
            {
                if (Time.time - checkTime >= 0.1f)
                {
                    FadeOutAlpha = new Color(1, 1, 1, FadeOutAlpha.a + 0.05f);
                    FadeOut.color = FadeOutAlpha;
                    checking = false;
                }
            }
            if (FadeOutAlpha.a >= 1.0f)
            {
                FadeOutAlpha = new Color(1, 1, 1, 1);
                FadeOut.color = FadeOutAlpha;
                changeScene = false;
            }
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
            playerIsClose = false;
            Blink.SetActive(false);
            GameCharacter.talking = false;
            zeroText();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("In_Body");
    }

    void PlayerRePosition()
    {
        player.transform.position = new Vector2(295.67f, -40.78f);
    }

    public IEnumerator waitforsec()
    {
        doOnce = true;
        yield return new WaitForSeconds(2.1f);
        player.portalOnce = true;
        PlayerRePosition();
        // playerStop은 InBody들어가고 난 후에 영상 끝나고 움직이게
        LoadScene();
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
            player.stop = true;
            changeScene = true;
            
            if (changeScene)
            {
                if (!doOnce)
                {
                    StartCoroutine(waitforsec());
                }
                if (!checking)
                {
                    checkTime = Time.time;
                    checking = true;
                }
                else
                {
                    if (Time.time - checkTime >= 0.1f)
                    {
                        FadeOutAlpha = new Color(1, 1, 1, FadeOutAlpha.a + 0.05f);
                        FadeOut.color = FadeOutAlpha;
                        checking = false;
                    }
                }
                if (FadeOutAlpha.a >= 1.0f)
                {
                    FadeOutAlpha = new Color(1, 1, 1, 1);
                    FadeOut.color = FadeOutAlpha;
                    changeScene = false;
                }
            }
            zeroText();
        }
    }
}
