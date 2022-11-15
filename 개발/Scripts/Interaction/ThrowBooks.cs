using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBooks : MonoBehaviour
{
    Character Character;

    bool checkTime = false;
    bool DoOnce = false;
    float x, y;
    float nowTime;
    Vector3 charPosition;
    // Start is called before the first frame update
    void Start()
    {
        Character = GameObject.Find("Character").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!checkTime)
        {
            nowTime = Time.time;
            charPosition = Character.transform.position;
            x = charPosition.x + charPosition.x + charPosition.x - 295.76f - 295.76f;
            y = charPosition.y + charPosition.y + 113.8f + charPosition.y + 113.8f;
            DoOnce = false;
            checkTime = true;
        }
        else
        {
            if (Time.time - nowTime >= 2.0f)
            {
                Vector2 goal = new Vector2(x, y);
                transform.position = Vector2.MoveTowards(transform.position, goal, 0.3f);
                StartCoroutine(checkTimeFalse());
            }
        }
    }

    private IEnumerator checkTimeFalse()
    {
        if (!DoOnce)
        {
            DoOnce = true;
            yield return new WaitForSeconds(2.0f);
            checkTime = false;
        }
    }
}
