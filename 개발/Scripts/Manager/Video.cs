using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    private HP HP;

    float currentTime = 0;
    float checkTime = 0;

    private bool doOnce = false;
    private bool timeCheck = false;
    public GameObject VideoPanel;
    public Image GameOverPanel;
    public RawImage mScreen = null;
    public VideoPlayer mVideoPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        if(mScreen != null && mVideoPlayer != null)
        {
            StartCoroutine(PrepareVideo());
        }

        HP = GameObject.Find("HP").GetComponent<HP>();
    }

    protected IEnumerator PrepareVideo()
    {
        mVideoPlayer.Prepare();

        while(!mVideoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.5f);
        }
        mScreen.texture = mVideoPlayer.texture;
    }

    public void PlayVideo()
    {
        if (mVideoPlayer != null && mVideoPlayer.isPrepared)
        {
            mVideoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        if(mVideoPlayer != null && mVideoPlayer.isPrepared)
        {
            mVideoPlayer.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HP.nowHp <= 0 && !doOnce)
        {
            doOnce = true;
            VideoPanel.SetActive(true);
            PlayVideo();
            checkTime = Time.time;
            Invoke("FadeOut", 2.0f);
        }
    }

    void FadeOut()
    {
        Debug.Log("In");
        Color alpha = GameOverPanel.color;
        Debug.Log(Time.time - checkTime);
        /*while (alpha.a < 255)
        {
            *//*endTime = Time.time;
            if(endTime - startTime > 0.5f)
            {
                Debug.Log("check");
                alpha.a = 255;
                startTime = Time.time;
                //Debug.Log(Time.time);
            }*//*

            if (!timeCheck)
            {
                checkTime = Time.time;
                timeCheck = true;
            }
            if (Time.time - checkTime >= 1)
            {
                alpha.a += 100;
                timeCheck = false;
            }

            GameOverPanel.color = alpha;
        }*/
        
        Debug.Log("Done");
    }
}
