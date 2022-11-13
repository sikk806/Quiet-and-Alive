using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drown : MonoBehaviour
{
    public bool onBone = false;

    float checkTime;

    HP HP;
    Character Character;

    // Start is called before the first frame update
    void Start()
    {
        HP = GameObject.Find("HP").GetComponent<HP>();
        Character = GameObject.Find("Character").GetComponent<Character>();
        checkTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // 뼈 위에 있으면 안전
        if(!onBone)
        {

            Character.anim.SetBool("UpOo", true);
            if(Time.time - checkTime >= 1.0f)
            {
                HP.nowHp -= 10;
                checkTime = Time.time;
            }
        }
        else
        {
            Character.anim.SetBool("UpOo", false);

        }
    }
}
