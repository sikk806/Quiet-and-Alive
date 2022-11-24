using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EP : MonoBehaviour
{
    [SerializeField]
    private Slider ep;
    string SceneName = "";

    private float checkTime;
    private float maxEp = 3.0f;
    public float nowEp = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(nowEp);
    }

    // Update is called once per frame
    void Update()
    {
        SceneName = SceneManager.GetActiveScene().name;
        if (SceneName == "In_Body")
        {
            renewEP();
        }
    }

    private void renewEP()
    {
        ep.value = (float)nowEp / (float)maxEp;
    }
}
