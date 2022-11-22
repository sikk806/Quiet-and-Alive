using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_DarkRoom : MonoBehaviour
{
    bool playerIsClose = false;
    public int PortalNo = 0;
    public Character Character;
    public GameObject Heart;

    // DarkRoom, BoneStart, MazeStart, HeartStart
    float[] x = { 407.7f, 358.0f, 347.0f, 251.5f };
    float[] y = { -82.2f, -112.0f, -229.0f, -169.0f };

    // Start is called before the first frame update
    void Start()
    {
        Character = GameObject.Find("Character").GetComponent<Character>();
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
        Character.transform.position = new Vector3(x[0], y[0], 1.0f);
        Character.stop = true;
        yield return new WaitForSeconds(2.0f);
        Character.stop = false;
        
    }
}
