using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    private string SceneName = "";
    private bool pressJump = false;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        // 좌우 버튼
        if(Input.GetButtonDown("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*10.0f, rigid.velocity.y);
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x, rigid.velocity.y);
        }

        // 방향 바꾸는 코드
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        if(Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }
        if(Input.GetButtonDown("Jump"))
        {
            anim.SetBool("isJump", true);
            pressJump = true;
        }
        else if(Mathf.Abs(rigid.velocity.y) <= 0 && pressJump == true)
        {
            anim.SetBool("isJump", false);
            pressJump = false;
        }
            
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // horizontal 입력 받아서 움직이는 코드
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }





    }
}
