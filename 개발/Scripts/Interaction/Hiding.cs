using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hiding : MonoBehaviour
{
    private GameObject Portal;
    public GameObject GlitterProps;
    public bool CanHide = false;
    public bool Hide = true;
    Character GameCharacter;

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        Portal = GameObject.Find("Portal_to2F");
    }

    // Update is called once per frame
    void Update()
    {
        if(CanHide)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Portal.SetActive(true);
                Hide = true;
                GameCharacter.hiding = true;
                GameCharacter.anim.SetBool("isHide", true);
            }
            else if(Input.GetKeyUp(KeyCode.Z))
            {
                Hide = false;
                GameCharacter.hiding = false;
                GameCharacter.anim.SetBool("isHide", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GlitterProps.SetActive(true);
            CanHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GlitterProps.SetActive(false);
            CanHide = false;
        }

    }
}
