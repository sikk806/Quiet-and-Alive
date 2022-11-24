using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MEBackGround : MonoBehaviour
{
    string SceneName = "";
    bool doOnce = false;
    public Image BG;
    public GameObject EP;
    EP EPs;

    // Start is called before the first frame update
    void Start()
    {
        EPs = EP.GetComponent<EP>();
    }

    // Update is called once per frame
    void Update()
    {
        SceneName = SceneManager.GetActiveScene().name;
        if(SceneName == "In_Body")
        {
            if (!doOnce)
            {
                EP.SetActive(true);
                doOnce = true;
            }
            BG.rectTransform.sizeDelta = new Vector2(176.2386f, 50.6356f);
        }
        else
        {
            EP.SetActive(false);
            BG.rectTransform.sizeDelta = new Vector2(176.2386f, 22.3959f);
            BG.rectTransform.anchoredPosition = new Vector2(86.58541f, -13.61527f);
        }
    }
}
