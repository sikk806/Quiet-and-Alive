using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalThroatToBody : MonoBehaviour
{
    public bool playerIsClose;
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

    void PlayerRePosition()
    {
        player.transform.position = new Vector2(0, -53.0f);
    }
}
