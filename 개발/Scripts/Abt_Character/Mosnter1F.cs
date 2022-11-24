using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mosnter1F : MonoBehaviour
{
    GameObject Left;
    GameObject Right;
    SpriteRenderer spriteRenderer;
    Character character;
    HP HP;


    private Animator anim;

    public float speed;

    private bool Attack = false;
    public bool check = false;
    bool fall = true;
    bool opposite = false;
    public bool playerIsClose = false;

    // Start is called before the first frame update
    void Start()
    {
        Left = GameObject.Find("movePoint_L");
        Right = GameObject.Find("movePoint_R");
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        character = GameObject.Find("Character").GetComponent<Character>();
        HP = GameObject.Find("HP").GetComponent<HP>();
        anim.speed = 0.0f;
        Debug.Log(Right.transform.position);
        StartCoroutine(Falling());
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            if (fall)
            {
                Vector3 right = new Vector3(Right.transform.position.x, Right.transform.position.y, -3.0f);
                transform.position = Vector3.MoveTowards(transform.position, Right.transform.position, speed);
                if (transform.position.y <= -5.4f)
                {
                    anim.SetBool("startFalling", false);
                    Attack = true;
                }
            }
            else
            {
                if (!opposite && character.anim.GetBool("isHide"))
                {
                    speed = 0.02f;
                    spriteRenderer.flipX = false;
                    transform.position = Vector3.MoveTowards(transform.position, Left.transform.position, speed);
                    if(transform.position == Left.transform.position)
                    {
                        opposite = true;
                    }
                }
                else if(opposite && character.anim.GetBool("isHide"))
                {
                    speed = 0.02f;
                    spriteRenderer.flipX = true;
                    transform.position = Vector3.MoveTowards(transform.position, Right.transform.position, speed);
                    if (transform.position == Right.transform.position)
                    {
                        opposite = false;
                    }
                }
                else if(!character.anim.GetBool("isHide"))
                {
                    speed = 0.1f;
                    Vector3 chase = new Vector3(character.transform.position.x, transform.position.y, -3.0f);
                    if(transform.position.x > character.transform.position.x)
                    {
                        spriteRenderer.flipX = false;
                        transform.position = Vector3.MoveTowards(transform.position, chase, speed);
                    }
                    else
                    {
                        spriteRenderer.flipX = true;
                        transform.position = Vector3.MoveTowards(transform.position, chase, speed);
                    }
                }
            }
        }

        if(playerIsClose && !character.anim.GetBool("isHide"))
        {
            HP.nowHp = 0;
        }
    }

    IEnumerator Falling()
    {
        yield return new WaitForSeconds(5.0f);
        anim.SetBool("wait", false);
        anim.speed = 1.0f;
        yield return new WaitForSeconds(1.1f);
        anim.SetBool("startFalling", true);
        transform.position = new Vector2(transform.position.x, 14.0f);
        check = true;
        yield return new WaitForSeconds(5.75f);
        anim.SetBool("walking", true);
        fall = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Attack)
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
}