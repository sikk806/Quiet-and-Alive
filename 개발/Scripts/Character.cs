using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    float h;
    float v;
    public float maxSpeed;
    public float jumpPower;
    private string SceneName = "";
    private bool pressJump = false;
    private float jumpTime;
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

    void Start()
    {
        
    }

    void Update()
    {
        // Scene �̸� ��������
        SceneName = SceneManager.GetActiveScene().name;
        // horizontal �Է� �޾Ƽ� �����̴� �ڵ�
        h = Input.GetAxisRaw("Horizontal");
        if (SceneName == "In_Body" || SceneName == "MirrorPlace")
        {
            // ������ ������ Jump ����
            anim.SetBool("isJump", false);
            pressJump = false;

            v = Input.GetAxisRaw("Vertical");

            if (Input.GetButton("Horizontal"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }

            // �� / ��
            if ((rigid.velocity.x) == 0)
            {
                anim.SetBool("isWalk", false);
            }
            else
            {
                anim.SetBool("isWalk", true);
            }

            // �� / �Ʒ�
            if((rigid.velocity.y) > 0)
            {
                anim.SetBool("isWalk", false);
                anim.SetBool("WalkBack", true);
            }
            else if ((rigid.velocity.y) < 0)
            {
                anim.SetBool("isWalk", false);
                anim.SetBool("WalkFront", true);
            }
            else
            {
                anim.SetBool("WalkBack", false);
                anim.SetBool("WalkFront", false);
            }
            if(Input.GetButtonDown("Jump"))
            {
                jumpTime = Time.time;
                

            }

        }
        else
        {

            if (Input.GetButtonDown("Jump"))
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }

            // �¿� ��ư
            if (Input.GetButtonDown("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 10.0f, rigid.velocity.y);
            }
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x, rigid.velocity.y);
            }

            // ���� �ٲٴ� �ڵ�
            if (Input.GetButton("Horizontal"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }
            if (Mathf.Abs(rigid.velocity.x) < 0.3)
            {
                anim.SetBool("isWalk", false);
            }
            else
            {
                anim.SetBool("isWalk", true);
            }
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("isJump", true);
                pressJump = true;
            }
            else if (Mathf.Abs(rigid.velocity.y) <= 0 && pressJump == true)
            {
                anim.SetBool("isJump", false);
                pressJump = false;
            }
        }
            
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SceneName == "In_Body" || SceneName == "MirrorPlace")
        {
            rigid.gravityScale = 0;
            if(h != 0)
            {
                rigid.velocity = new Vector2(h * 10, 0);
            }
            else
            {
                rigid.velocity = new Vector2(0, v * 10);
            }
        }
        else
        {
            rigid.gravityScale = 2;
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
}
