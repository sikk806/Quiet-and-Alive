using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{

    [SerializeField]
    private Slider hp;

    private float checkTime;
    private float maxHp = 100;
    private float nowHp = 100;
    private bool check = false;
    private bool timeCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        hp.value = (float)nowHp / (float)maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(!check && hp.value < 1)
        {
            nowHp = 100;
        }
        if (Input.GetButtonDown("Submit") || check)
        {
            if (!timeCheck)
            {
                checkTime = Time.time;
                timeCheck = true;
            }
            check = true;
            if (Time.time - checkTime >= 1)
            {
                nowHp -= 10;
                timeCheck = false;
            }
        }
        if (Input.GetButtonUp("Submit"))
        {
            check = false;
        }

        renewHP();
    }

    private void renewHP()
    {
        hp.value = (float)nowHp / (float)maxHp;
    }
}
