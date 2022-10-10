using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalInBodyToLib1 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerIsClose;
    public int iLeveltoLoad;
    public string sLeveltoLoad;
    public bool useIntegerToLoadLevel = true;
    public Character player;

    void Start()
    {
        player = FindObjectOfType<Character>();
    }


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
        SceneManager.LoadScene("Library2");
    }

    void PlayerRePosition()
    {
        player.transform.position = new Vector2(-29.0f, -8.0f);
    }
}
