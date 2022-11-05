using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    List<Resolution> resolutions = new List<Resolution>();
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Screen.SetResolution(854, 480, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitUI()
    {
        resolutions.AddRange(Screen.resolutions);
        foreach(Resolution item in resolutions)
        {
            Debug.Log(item.width + "x" + item.height + " " + item.refreshRate);
        }
    }
}
