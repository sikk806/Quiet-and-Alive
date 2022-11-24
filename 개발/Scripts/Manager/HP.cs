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
    public float nowHp = 100;



    // Start is called before the first frame update
    void Start()
    {
        hp.value = (float)nowHp / (float)maxHp;
        nowHp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.H))
        {
            nowHp = 100;
        }
        

        renewHP();
    }

    private void renewHP()
    {
        hp.value = (float)nowHp / (float)maxHp;
    }
}
