using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{



    float h;
    float keep_h;
    float v;
    public float maxSpeed;
    public float jumpPower;
    public bool talking = false; // ��ȭ�߿��� ����Ű ����
    private string SceneName = "";
    private bool jumping = false; // ���� �������θ� ���� ����
    private bool pressJump = false; // ���� ���� ����
    private bool overJump = false; // ��ٸ� �������鼭 ���� ���� ����
    private float jumpTime = 0; // Jump �ð� ����
    private float origin_Y;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    AudioSource AudioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Scene �̸� ��������
        SceneName = SceneManager.GetActiveScene().name;
        // horizontal �Է� �޾Ƽ� �����̴� �ڵ�
        keep_h = h;
        h = Input.GetAxisRaw("Horizontal");
        if(jumping  == true)
        {
            h = keep_h;
        }
        if(talking)
        {
            h = 0;
        }
        if (SceneName == "In_Body" || SceneName == "MirrorPlace")
        {
            // ������ ������ Jump ����
            /*anim.SetBool("isJump", false);*/
            pressJump = false;

            //��, �Ʒ�
            v = Input.GetAxisRaw("Vertical");
            if(talking)
            {
                v = 0;
            }

            if (Input.GetButton("Horizontal"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }

            // �� / ��
            //Debug.Log("vel.x : " + rigid.velocity.x);
            if (Mathf.Abs(rigid.velocity.x) < 0.3 && !jumping)
            {
                anim.SetBool("isWalk", false);
            }
            else if(Mathf.Abs(rigid.velocity.x) >= 0.3 && !jumping)
            {
                anim.SetBool("isWalk", true);
            }

            // �� / �Ʒ�
            if ((rigid.velocity.y) > 0 && !jumping)
            {
                anim.SetBool("isWalk", false);
                anim.SetBool("WalkBack", true);
            }
            else if ((rigid.velocity.y) < 0 && !jumping)
            {
                anim.SetBool("isWalk", false);
                anim.SetBool("WalkFront", true);
            }
            else if ((rigid.velocity.y) == 0 && !jumping)
            {
                anim.SetBool("WalkBack", false);
                anim.SetBool("WalkFront", false);
            }

            if (Input.GetButton("Jump"))
            {
                anim.SetBool("isJump", true);
                //jumping = true;
            }

            // **********************************************
            // JUMP ���� �ٲٰ� �ִϸ��̼��� �̻��Ҷ� �ٲ㺼��
            // **********************************************
            else if (rigid.velocity.y <= -18.5f && jumping)
            {

                Debug.Log("check : " + rigid.velocity.y);
                Debug.Log("check-not jump");
                anim.SetBool("isJump", false);
                //jumping = false;
            }
        }
        else
        {
            
            if (Input.GetButtonDown("Jump") && pressJump == false && !talking)
            {
                AudioSource.Play();
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            else if(talking)
            {
                
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
            if (pressJump == false)
            {
                rigid.gravityScale = 0;
            }
            if(h != 0)
            {
                rigid.velocity = new Vector2(h * 10, 0);
            }
            else
            {
                rigid.velocity = new Vector2(0, v * 10);
            }

            if (Input.GetButton("Jump") && overJump == false)
            {
                overJump = true;
                jumping = true;
                pressJump = true;
            }

            if(jumping)
            {
                rigid.gravityScale = -100.0f;
                jumpTime += Time.deltaTime;
                rigid.gravityScale += jumpTime * 200.0f;
            }

            if(rigid.gravityScale >= 98.0f)
            {
                jumping = false;
                jumpTime = 0.0f;
                overJump = false;
                pressJump = false;
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
