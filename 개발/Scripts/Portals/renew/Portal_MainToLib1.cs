using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class Portal_MainToLib1 : MonoBehaviour
{

    private bool playerIsClose;
    public Character player;
    VideoPlay VideoPlay;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Character>();
        VideoPlay = GameObject.Find("VideoController").GetComponent<VideoPlay>();
    }

    // Update is called once per frame
    void Update()
    {
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

        SceneManager.LoadScene("Library1");
    }

    void PlayerRePosition()
    {
        player.transform.position = new Vector2(-14.19f, -6.3f);
    }
}
