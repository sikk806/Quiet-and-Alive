using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class Portal_MainToLib1 : MonoBehaviour
{

    private bool playerIsClose;
    public Character player;
    VideoPlay VideoPlay;

    public GameObject check1;
    public GameObject check2;
    public GameObject check3;
    public GameObject check4;
    curtainSound ch1;
    ForNoSoundCheckVer ch2;
    ForNoSoundCheckVer ch3;
    ForNoSoundCheckVer ch4;

    [Header("±ôºýÀÌ´Â ¼Óµµ(0~1)")]
    public float blinkSpeed;

    private bool checkTime = false;
    private bool dark = true;
    private float timeCheck;

    public GameObject Blink;
    SpriteRenderer BlinkSr;
    Color BlinkColor;



    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Character>();
        VideoPlay = GameObject.Find("VideoController").GetComponent<VideoPlay>();
        ch1 = check1.GetComponent<curtainSound>();
        ch2 = check2.GetComponent<ForNoSoundCheckVer>();
        ch3 = check3.GetComponent<ForNoSoundCheckVer>();
        ch4 = check4.GetComponent<ForNoSoundCheckVer>();
        BlinkSr = Blink.GetComponent<SpriteRenderer>();
        BlinkColor = BlinkSr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkTime)
        {
            timeCheck = Time.time;
            checkTime = true;
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

        if (Input.GetKeyDown(KeyCode.Z) && playerIsClose)
        {
            if (playerIsClose == true)
            {
                VideoPlay.waitTime = true;
                if (player != null)
                {
                    PlayerRePosition();
                }
                LoadScene();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && ch1.check && ch2.check && ch3.check && ch4.check)
        {
            playerIsClose = true;
            Blink.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Blink.SetActive(false);
            playerIsClose = false;
        }
    }

    void LoadScene()
    {

        SceneManager.LoadScene("Library1");
    }

    void PlayerRePosition()
    {
        player.transform.position = new Vector2(-14.19f, -6.3f);
    }
}
