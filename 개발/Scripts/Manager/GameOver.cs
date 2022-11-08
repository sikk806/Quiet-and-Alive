using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    float checkTime = 0;
    bool timeCheck = false;
    public Image GameoverPanel;
    public Text GameoverText;
    public bool Gameover = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Gameover)
        {
            Color alpha = GameoverPanel.color;
            Color alpha_t = GameoverText.color;

            if(!timeCheck)
            {
                timeCheck = true;
                checkTime = Time.time;
            }
            if(Time.time - checkTime >= 0.01f)
            {
                Debug.Log(checkTime);
                if(alpha.a < 1)
                {
                    alpha.a += 0.005f;
                    alpha_t.a += 0.005f;
                    Debug.Log(alpha.a);
                    GameoverPanel.color = alpha;
                    GameoverText.color = alpha_t;
                    timeCheck = false;
                }
                else
                {
                    Gameover = false;
                }
            }
        }
    }
}
