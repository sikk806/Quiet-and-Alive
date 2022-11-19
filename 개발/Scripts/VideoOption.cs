using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoOption : MonoBehaviour
{
    private string SceneName = "";
    public GameObject[] Canvas;

    List<Resolution> resolutions = new List<Resolution>();
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.FindGameObjectsWithTag("UIs");
        Screen.SetResolution(854, 480, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneName == "Main")
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            DeleteCanvas();
        }
    }

    void InitUI()
    {
        resolutions.AddRange(Screen.resolutions);
        foreach(Resolution item in resolutions)
        {
            Debug.Log(item.width + "x" + item.height + " " + item.refreshRate);
        }
    }

    private void DeleteCanvas()
    {
        if(Canvas.Length >1)
        {
            Destroy(Canvas[1]);
        }
    }
}
