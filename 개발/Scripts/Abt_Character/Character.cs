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
    bool doOnce = false;

    public GameObject DestroyObject;
    public GameObject ActiveObject;
    public GameObject Portal;
    public GameObject Monster;

    public bool talking = false; // 대화중에는 방향키 금지
    public bool pressJump = false; // 더블 점프 방지 , 사다리에서 점프 방지
    public bool portalOnce = false;
    public bool portalTwice = false;
    public bool CanGoUp = false;
    public bool hiding = false;
    public float maxSpeed;
    public float jumpPower;

    private string SceneName = "";
    private bool jumping = false; // 한쪽 방향으로만 점프 유지
    private bool overJump = false; // 쯔꾸르 떨어지면서 점프 시작 방지
    private float jumpTime = 0; // Jump 시간 측정
    private float origin_Y;
    //private Ladder Ladder;

    public Animator anim;
    public Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    AudioSource AudioSource;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(!talking)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (SceneName == "Main")
        {
            Destroy(gameObject);
        }
        // Scene 이름 가져오기
        SceneName = SceneManager.GetActiveScene().name;
        
        // horizontal 입력 받아서 움직이는 코드
        keep_h = h;
        // h = Input.GetAxisRaw("Horizontal");
        if(jumping  == true)
        {
            h = keep_h;
        }
        if (talking || hiding || anim.GetBool("isLib"))
        {
            h = 0;
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal");
        }

        // 1층에서 2층으로 올라갈때
        if (SceneName == "Library1_2F" && portalOnce)
        {
            portalOnce = false;
            if(portalTwice)
            {
                DestroyObject = GameObject.Find("MonsterIdle");
                DestroyObject.SetActive(false);
                DestroyObject = GameObject.Find("BandageBookPortal/BandageBook");
                DestroyObject.SetActive(true);
                portalTwice = false;
            }
            anim.SetBool("GoIdle", false);
            anim.SetBool("isClibing", false);
            anim.SetBool("StopClibing", false);
            rigid.velocity = new Vector2(0, 0);
        }
        
        // 2층에서 1층으로 내려갈때
        if(SceneName == "Library1" && portalOnce)
        {
            Portal = GameObject.Find("Portal_to2F");
            Portal.SetActive(false);
            portalOnce = false;
            portalTwice = true;
            anim.SetBool("isClibing", true);
            anim.SetBool("StopClibing", true);
        }
        /*if(SceneName == "Library1" && portalOnce)
        {
            portalOnce = true;
        }*/
        if (SceneName == "In_Body" || SceneName == "MirrorPlace")
        {
            anim.SetBool("IB", true);
            // 도서관 에서의 Jump 삭제
            /*anim.SetBool("isJump", false);*/
            pressJump = false;

            //위, 아래
            v = Input.GetAxisRaw("Vertical");
            if(talking)
            {
                v = 0;
            }

            if (Input.GetButton("Horizontal"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }

            // 좌 / 우
            //Debug.Log("vel.x : " + rigid.velocity.x);
            if (Mathf.Abs(rigid.velocity.x) < 0.3 && !jumping)
            {
                anim.SetBool("isWalk", false);
            }
            else if(Mathf.Abs(rigid.velocity.x) >= 0.3 && !jumping)
            {
                anim.SetBool("isWalk", true);
            }

            // 위 / 아래
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
            // JUMP 높이 바꾸고 애니메이션이 이상할때 바꿔볼것
            // **********************************************
            else if (rigid.velocity.y <= -18.5f && jumping)
            {
                anim.SetBool("isJump", false);
                //jumping = false;
            }
        }
        else
        {
            anim.SetBool("IB", false);
            if (SceneName == "Library1" && !doOnce)
            {
                doOnce = true;
                StartCoroutine(waitforsec());
            }
            if (Input.GetButtonDown("Jump") && pressJump == false && !talking)
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            else if(talking)
            {
                rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }

            // 좌우 버튼
            if (Input.GetButtonDown("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 10.0f, rigid.velocity.y);
            }
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x, rigid.velocity.y);
            }

            // 방향 바꾸는 코드
            if (Input.GetButton("Horizontal") && !talking && !hiding && !anim.GetBool("isLib"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }
            if (Mathf.Abs(rigid.velocity.x) < 0.1)
            {
                anim.SetBool("isWalk", false);
            }
            else if(rigid.position.y > -6.0f && CanGoUp)
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

    public IEnumerator waitforsec()
    {
        anim.SetBool("isLib", true);
        yield return new WaitForSeconds(1.8f);
        anim.SetBool("isLib", false);
    }
}
