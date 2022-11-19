using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    float v;
    //public bool CanGoUp = false;
    public bool isFalling = false;

    private HP HP;

    Character GameCharacter;
    

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        HP = GameObject.Find("HP").GetComponent<HP>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameCharacter.CanGoUp)
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
            if (Input.GetButton("Vertical"))
            {
                /*if(GameCharacter.rigid.position.y <= 0)
                {
                    isFalling = false;
                }*/
                GameCharacter.anim.SetBool("GoIdle", false);
                GameCharacter.rigid.gravityScale = 0;
                GameCharacter.rigid.velocity = new Vector2(GameCharacter.rigid.velocity.x, GameCharacter.rigid.velocity.y * 10.0f);
                GameCharacter.anim.SetBool("isClibing", true);
                GameCharacter.anim.SetBool("StopClibing", false);
            }
            if(Input.GetButtonUp("Vertical"))
            {
                GameCharacter.rigid.velocity = new Vector2(GameCharacter.rigid.velocity.x, GameCharacter.rigid.velocity.y);
                GameCharacter.anim.SetBool("StopClibing", true);
            }
        }
        else
        {
            GameCharacter.rigid.gravityScale = 2;
            if(GameCharacter.rigid.position.y > -6.0f)
            {
                GameCharacter.anim.SetBool("isClibing", false);
                GameCharacter.anim.SetBool("StopClibing", false);
            }

            if(GameCharacter.rigid.position.y < -6.1f)
            {
                GameCharacter.pressJump = false;
                GameCharacter.anim.SetBool("isClibing", false);
                GameCharacter.anim.SetBool("GoIdle", true);
                if(isFalling)
                {
                    HP.nowHp -= 50;
                }
                isFalling = false;
            }
            if (Mathf.Abs(GameCharacter.rigid.velocity.x) < 0.1)
            {
                GameCharacter.anim.SetBool("isWalk", false);
            }
            else
            {
                GameCharacter.anim.SetBool("isWalk", true);
            }
        }
    }

    void FixedUpdate()
    {
        if (GameCharacter.CanGoUp)
        {
            GameCharacter.rigid.velocity = new Vector2(GameCharacter.rigid.velocity.x, v * 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameCharacter.CanGoUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameCharacter.CanGoUp = false;
        }
    }
}
