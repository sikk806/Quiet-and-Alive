using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBooks : MonoBehaviour
{
    Character Character;
    GameObject temp;

    int ranNum;
    bool checkTime = false;
    public bool endTalk = false;
    public float throwSpeed;
    public GameObject book;
    public Rigidbody2D bookRB;
    float x, y;
    float nowTime;
    Vector2 goal;
    Vector3 charPosition;
    Vector3 defaultBookPosition;
    // Start is called before the first frame update
    void Start()
    {
        //bookRB = book.GetComponent<Rigidbody2D>();
        Character = GameObject.Find("Character").GetComponent<Character>();
        defaultBookPosition = transform.position;
        goal = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (endTalk && Character.anim.GetBool("JumpMap")) 
        {
            if (!checkTime)
            {
                nowTime = Time.time;
                checkTime = true;
            }
            else
            {
                if (Time.time - nowTime >= 2.0f)
                {
                    temp = Instantiate(book);
                    Destroy(temp, 4.0f);
                    temp.transform.position = gameObject.transform.position;
                    goal = Character.transform.position - transform.position;
                    goal = goal.normalized;
                    bookRB = temp.GetComponent<Rigidbody2D>();
                    checkTime = false;
                }
            }
            if (temp != null)
            {
                bookRB.velocity = goal * throwSpeed;
            }
        }
    }
}
