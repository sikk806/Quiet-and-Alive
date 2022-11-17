using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    float checkTime = 0;
    bool timeCheck = false;
    public Image GameoverPanel;
    public Text GameoverText;
    public bool Gameover = false;
    Color alpha;
    Color alpha_t;
    HP HP;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        HP = GameObject.Find("HP").GetComponent<HP>();
        alpha = GameoverPanel.color;
        alpha_t = GameoverText.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP.nowHp <= 0)
        {
            Gameover = true;
        }
        else if (HP.nowHp > 0)
        {
            alpha.a = 0.0f;
            alpha_t.a = 0.0f;
            GameoverPanel.color = alpha;
            GameoverText.color = alpha_t;
        }
        if (Gameover)
        {

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
        if(alpha.a >= 1.0f)
        {
            alpha.a = 0.0f;
            alpha_t.a = 0.0f;
            GameoverPanel.color = alpha;
            GameoverText.color = alpha_t;
            HP.nowHp = 100;
            Gameover = false;
            SceneManager.LoadScene("Main");
        }
    }
}
