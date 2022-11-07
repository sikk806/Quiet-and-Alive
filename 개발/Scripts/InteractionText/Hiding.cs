using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hiding : MonoBehaviour
{
    public GameObject GlitterProps;
    public bool CanHide = false;
    public bool Hide = true;
    Character GameCharacter;

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CanHide)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Hide = true;
                GameCharacter.anim.SetBool("isHide", true);
            }
            else if(Input.GetKeyUp(KeyCode.Z))
            {
                Hide = false;
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
