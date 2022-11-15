using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraRange : MonoBehaviour
{
    //public SpriteRenderer renderer;
    private Vector3 cameraBoundary;
    public Transform PlayerTrans;
    public GameObject PlayerObj;
    public GameObject BackGround;
    public float MinX, MinY, MaxX, MaxY, bgSizeX, bgSizeY;
    float CameraWidth, CameraHeight;
    private string SceneName = "";

    void Start()
    {
        CameraWidth = Camera.main.aspect * Camera.main.orthographicSize;
        CameraHeight = Camera.main.orthographicSize;

        Debug.Log(CameraWidth + " " + CameraHeight);

        BackGround = GameObject.FindGameObjectWithTag("BackGround");
        bgSizeX = BackGround.GetComponent<Renderer>().bounds.size.x;
        bgSizeY = BackGround.GetComponent<Renderer>().bounds.size.y;

        //Debug.Log(BackGround.transform.position.x);

        MinX = BackGround.transform.position.x - bgSizeX / 2.0f;
        MinY = BackGround.transform.position.y - bgSizeY / 2.0f;
        MaxX = BackGround.transform.position.x + bgSizeX / 2.0f;
        MaxY = BackGround.transform.position.y + bgSizeY / 2.0f;

        //Debug.Log("bgSizeX : " + bgSizeX + " bgSizeY : " + bgSizeY + " MinX : " + MinX + " MaxX : " + MaxX + " MinY : " + MinY + " MaxY : " + MaxY);

        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerTrans = PlayerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneName == "Main")
        {
            Destroy(gameObject);
        }
        BackGround = GameObject.FindGameObjectWithTag("BackGround");
        bgSizeX = BackGround.GetComponent<Renderer>().bounds.size.x;
        bgSizeY = BackGround.GetComponent<Renderer>().bounds.size.y;

        //Debug.Log(BackGround.transform.position.x);

        MinX = BackGround.transform.position.x - bgSizeX / 2.0f;
        MinY = BackGround.transform.position.y - bgSizeY / 2.0f;
        MaxX = BackGround.transform.position.x + bgSizeX / 2.0f;
        MaxY = BackGround.transform.position.y + bgSizeY / 2.0f;
        
        cameraBoundary = new Vector3(Mathf.Clamp(PlayerTrans.position.x, MinX + CameraWidth, MaxX - CameraWidth),
            Mathf.Clamp(PlayerTrans.position.y, MinY + CameraHeight, MaxY - CameraHeight),
            -10);
        transform.position = Vector3.MoveTowards(transform.position, cameraBoundary, 1.0f);
    }
}
