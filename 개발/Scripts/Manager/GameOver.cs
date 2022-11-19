using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    bool timeCheck = false;
    bool doOnce = false;
    float checkTime = 0;

    public Text GameoverText;
    public Image GameoverPanel;

    public bool Gameover = false;

    VideoPlay VideoPlay;
    HP HP;
    Color alpha_t;
    Color alpha;

    // Start is called before the first frame update
    void Start()
    {
        HP = GameObject.Find("HP").GetComponent<HP>();
        VideoPlay = GameObject.Find("VideoController").GetComponent<VideoPlay>();
        DontDestroyOnLoad(gameObject);
        alpha_t = GameoverText.color;
        alpha = GameoverPanel.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP.nowHp <= 0 && !doOnce)
        {
            doOnce = true;
            StartCoroutine(waitforsec());
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
            if(Time.time - checkTime >= 0.1f)
            {
                if(alpha.a < 1)
                {
                    alpha.a += 0.05f;
                    alpha_t.a += 0.05f;
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
            doOnce = false;
            SceneManager.LoadScene("Main");
        }
    }

    public IEnumerator waitforsec()
    {
        yield return new WaitForSeconds(0.5f);
        VideoPlay.waitTime = true;
    }
}
