using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMonster : MonoBehaviour
{
    int i;
    int points;

    bool go = false;
    bool Hit = false;
    bool checkTime = false;
    float timeCheck;

    public float speed;
    public List<Vector3> ways = new List<Vector3>();

    Animator anim;
    Character GameCharacter;
    HP HP;

    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        HP = GameObject.Find("HP").GetComponent<HP>();
        anim = GetComponent<Animator>();
        i = 0;
        points = ways.Count;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, ways[i], speed);
        if(!go)
        {
            if(transform.position == ways[i])
            {
                i++;
                if(i >= points)
                {
                    i -= 1;
                    StartCoroutine(waitforsec(true));
                }
            }
        }
        else
        {
            if(transform.position == ways[i])
            {
                i--;
                if(i < 0)
                {
                    i += 1;
                    StartCoroutine(waitforsec(false)) ;
                }
            }
        }

        if(Hit)
        {
            if(!checkTime)
            {
                timeCheck = Time.time;
                checkTime = false;
            }
            else
            {
                if(Time.time - timeCheck >= 1.0f)
                {
                    HP.nowHp -= 10;
                    checkTime = true;
                }
            }
        }
    }

    public IEnumerator waitforsec(bool check)
    {
        anim.SetBool("idle", true);
        yield return new WaitForSeconds(2.0f);
        anim.SetBool("idle", false);
        go = check;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Hit = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Hit = false;
        }
    }
}
