using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_DarkRoom : MonoBehaviour
{
    bool playerIsClose = false;
    public int PortalNo = 0;
    public Character Character;
    public GameObject Heart;
    fireFly fireFly;

    // DarkRoom, BoneStart, MazeStart, HeartStart
    float[] x = { 407.7f, 383.14f, 251.5f };
    float[] y = { -82.2f, -179.68f, -169.0f };

    // Start is called before the first frame update
    void Start()
    {
        Character = GameObject.Find("Character").GetComponent<Character>();
        fireFly = GameObject.Find("fireFly").GetComponent<fireFly>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsClose)
        {
            StartCoroutine(rePosition());
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

    

    private IEnumerator rePosition()
    {
        Character.anim.SetBool("JumpMap", false);
        if (CompareTag("ThroatToBody"))
        {
            PortalNo = 0;
            Character.stop = true;
            fireFly.fly = true;
            Character.transform.position = new Vector2(x[PortalNo], y[PortalNo]);
            yield return new WaitForSeconds(2.0f);
            Character.stop = false;
        }
        else if (CompareTag("BodyToVisera"))
        {
            PortalNo = 1;
            Character.transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
            Character.transform.position = new Vector2(x[PortalNo], y[PortalNo]);
        }
        else if (CompareTag("ViseraToHeart"))
        {
            PortalNo = 2;
            Heart.SetActive(false);
            Character.transform.position = new Vector2(x[PortalNo], y[PortalNo]);
        }
    }
}
