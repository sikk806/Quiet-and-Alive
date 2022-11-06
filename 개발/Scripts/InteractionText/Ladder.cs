using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    float v;
    public bool CanGoUp = false;
    public bool isFalling = false;
    
    Character GameCharacter;

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CanGoUp)
        {
            if(GameCharacter.rigid.position.y > 0)
            {
                isFalling = true;
            }
            if(GameCharacter.rigid.position.y > -6.0f)
            {
                GameCharacter.anim.SetBool("isJump", false);
            }
            GameCharacter.pressJump = true;
            v = Input.GetAxisRaw("Vertical");
            if (Input.GetButtonDown("Vertical"))
            {
                GameCharacter.rigid.gravityScale = 0;
                GameCharacter.rigid.velocity = new Vector2(GameCharacter.rigid.velocity.x, GameCharacter.rigid.velocity.y * 10.0f);
            }
            if(Input.GetButtonUp("Vertical"))
            {
                GameCharacter.rigid.velocity = new Vector2(GameCharacter.rigid.velocity.x, GameCharacter.rigid.velocity.y);
            }
        }
        else
        {
            GameCharacter.rigid.gravityScale = 2;

            if(GameCharacter.rigid.position.y < -6.0f)
            {
                GameCharacter.pressJump = false;
                if(isFalling)
                {
                    //대충 HP 깎이는 코드
                }
                isFalling = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (CanGoUp)
        {
            GameCharacter.rigid.velocity = new Vector2(GameCharacter.rigid.velocity.x, v * 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            CanGoUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            CanGoUp = false;
        }
    }
}
