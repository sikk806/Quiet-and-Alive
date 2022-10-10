using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLib2ToMirror : MonoBehaviour
{
    public bool playerIsClose;
    public Character player;

    void Start()
    {
        player = FindObjectOfType<Character>();
    }

    void Update()
    {
        if (playerIsClose == true)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
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
        SceneManager.LoadScene("MirrorPlace");
    }

    void PlayerRePosition()
    {
        player.transform.position = new Vector2(4.0f, -20.0f);
    }
}
