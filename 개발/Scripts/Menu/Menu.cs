using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Text START;
    private Text CONTINUE;
    private Text END;
    public Image Left;

    Vector2 ArrowStart = new Vector2(364.0f, 142.5f);
    Vector2 ArrowContinue = new Vector2(364.0f, 107.8f);
    Vector2 ArrowEnd = new Vector2(364.0f, 73.1f);

    int menu_no = 0;
    float v;

    AudioSource Aud;

    // Start is called before the first frame update
    void Start()
    {
        START = GameObject.Find("START").GetComponent<Text>();
        CONTINUE = GameObject.Find("CONTINUE").GetComponent<Text>();
        END = GameObject.Find("END").GetComponent<Text>();
        Aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menu_no == 0)
        {
            START.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            CONTINUE.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            END.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Aud.Play();
                Left.rectTransform.position = ArrowEnd;
                menu_no = 2;
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                Aud.Play();
                Left.rectTransform.position = ArrowContinue;
                menu_no = 1;
            }
        }
        else if(menu_no == 1)
        {
            CONTINUE.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            START.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            END.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Aud.Play();
                Left.rectTransform.position = ArrowStart;
                menu_no = 0;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Aud.Play();
                Left.rectTransform.position = ArrowEnd;
                menu_no = 2;
            }
        }
        else if(menu_no == 2)
        {
            END.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            CONTINUE.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            START.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Aud.Play();
                Left.rectTransform.position = ArrowContinue;
                menu_no = 1;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Aud.Play();
                Left.rectTransform.position = ArrowStart;
                menu_no = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Aud.Play();
            if(menu_no == 0)
            {
                SceneManager.LoadScene("LoadingScene");
            }
            else if (menu_no == 1)
            {

            }
            else if (menu_no == 2)
            {
                Application.Quit();
            }
        }
    }
}
