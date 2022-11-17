using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_InBodyToLib1 : MonoBehaviour
{
    private bool playerIsClose;
    public Character player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose == true )
        {
            if (player != null)
            {
                PlayerRePosition();
            }
            LoadScene();
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

        SceneManager.LoadScene("Library1_Clear");
    }

    void PlayerRePosition()
    {
        player.transform.position = new Vector2(-6.74f, -7.88f);
    }
}
