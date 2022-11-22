using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_outDarkRoom : MonoBehaviour
{
    bool playerIsClose = false;
    public int PortalNo = 0;
    public Character Character;
    public GameObject Heart;

    // DarkRoom, BoneStart, MazeStart, HeartStart
    float[] x = { 358.0f, 347.0f, 251.5f };
    float[] y = { -112.0f, -229.0f, -169.0f };

    // Start is called before the first frame update
    void Start()
    {
        Character = GameObject.Find("Character").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose)
        {
            rePosition();
            playerIsClose = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }



    private void rePosition()
    {
        Character.anim.SetBool("JumpMap", false);
        if (CompareTag("ThroatToBody"))
        {
            PortalNo = 0;
            Character.anim.SetBool("JumpMap", true);
        }
        else if (CompareTag("BodyToVisera"))
        {
            PortalNo = 1;
            Character.transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
        }
        else if (CompareTag("ViseraToHeart"))
        {
            PortalNo = 2;
            Heart.SetActive(false);
        }
        Character.transform.position = new Vector2(x[PortalNo], y[PortalNo]);
    }
}
