using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Lib1ToInBody : MonoBehaviour
{
    bool checking = false;
    float checkTime;
    private bool playerIsClose;
    private bool changeScene = false;
    private bool doOnce = false;
    public Character player;
    public GameObject checkMonster;
    public SpriteRenderer FadeOut;
    private Collider2D BookCollider;

    Color FadeOutAlpha;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Character>();
        BookCollider = gameObject.GetComponent<Collider2D>();
        FadeOutAlpha = FadeOut.color;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!checkMonster.activeSelf)
        {
            BookCollider.enabled = true;
        }
        else
        {
            BookCollider.enabled = false;
        }*/
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
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
}
