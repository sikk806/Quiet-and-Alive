using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Library1ToInBody : MonoBehaviour
{
    public bool playerIsClose;
    public int iLeveltoLoad;
    public string sLeveltoLoad;
    public bool useIntegerToLoadLevel = true;
    public Character player;

    // Start is called before the first frame update
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
        SceneManager.LoadScene("In_Body");
    }

    void PlayerRePosition()
    {
        player.transform.position = new Vector2(0, -4.0f);
    }
}
