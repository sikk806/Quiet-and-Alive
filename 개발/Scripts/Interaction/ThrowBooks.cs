using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBooks : MonoBehaviour
{
    Character Character;
    GameObject temp;

    int ranNum;
    bool checkTime = false;
    bool DoOnce = false;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            temp = Instantiate(book);
            Destroy(temp, 4.0f);
            temp.transform.position = gameObject.transform.position;
            goal = Character.transform.position - transform.position;
            goal = goal.normalized;
            bookRB = temp.GetComponent<Rigidbody2D>();
        }
        if (temp != null)
        {
            bookRB.velocity = goal * throwSpeed;
            //goal = new Vector2(0, 1);

            //temp.transform.position = Vector3.MoveTowards(temp.transform.position, goal, 0.5f);
            /*if(temp.transform.position == goal)
            {
                Destroy(temp);
            }*/

        }
    }
   /* private void throwBook()
    {
        var temp = Instantiate(book);
        temp.transform.position = gameObject.transform.position;
    }

    /*public IEnumerator waitforsec(book)
    {
        yield return new WaitForSeconds(2.1f);
    } */
}
