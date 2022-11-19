using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using static UnityEngine.Video.VideoPlayer;
using static UnityEngine.Video.VideoClip;

public class VideoPlay : MonoBehaviour
{
    bool checking = false;
    bool FadeIn = false;
    float checkTime;
    int clipNo = 0;
    string SceneName;

    private HP HP;

    public bool waitTime = true;
    public float clipLength;

    public RawImage VideoImage;
    public VideoClip[] vidClip;
    public VideoPlayer vidPlayer;

    Color VideoAlpha;
    GameOver GameOver;


    // Start is called before the first frame update
    void Start()
    {
        GameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
        HP = GameObject.Find("HP").GetComponent<HP>();
        VideoAlpha = VideoImage.color;
        vidPlayer.clip = vidClip[0];
    }

    // Update is called once per frame
    void Update()
    {
        SceneName = SceneManager.GetActiveScene().name;
        if (waitTime)
        {
            StartCoroutine(waitforsec());
        }
        if (FadeIn)
        {   
            if (!checking)
            {
                checkTime = Time.time;
                checking = true;
            }
            else
            {
                if (Time.time - checkTime >= 0.1f)
                {
                    VideoAlpha = new Color(1, 1, 1, VideoAlpha.a - 0.05f);
                    VideoImage.color = VideoAlpha;
                    checking = false;
                }
            }
            if (VideoAlpha.a <= 0.0f)
            {
                VideoAlpha = new Color(1, 1, 1, 0);
                VideoImage.color = VideoAlpha;
                FadeIn = false;
            }
        }
    }

    public IEnumerator waitforsec()
    {
        waitTime = false;
        VideoAlpha.a = 1;
        // Lib1 시작 영상
        if(SceneName == "Library1" && HP.nowHp > 0)
        {
            clipNo = 0;
            PlayTheVid();
            VideoImage.color = new Color(1, 1, 1, 1);
        }
        // Lib1 GameOver 영상
        else if(SceneName == "Library1" || SceneName == "Library1_2F")
        {
            if (HP.nowHp <= 0)
            {
                clipNo = 1;
                PlayTheVid();
                VideoImage.color = new Color(1, 1, 1, 1);
            }
        }
        if (SceneName == "In_Body" && HP.nowHp > 0)
        {
            clipNo = 2;
            PlayTheVid();
            VideoImage.color = new Color(1, 1, 1, 1);
            clipLength = (float)vidClip[clipNo].length;
            yield return new WaitForSeconds(clipLength +0.5f);
            clipNo = 3;
            PlayTheVid();
        }
        clipLength = (float)vidClip[clipNo].length;
        yield return new WaitForSeconds(clipLength-0.8f);
        FadeIn = true;
        if(HP.nowHp <= 0)
        {
            GameOver.Gameover = true;
        }
    }

    private void PlayTheVid()
    {
        vidPlayer.clip = vidClip[clipNo];
        vidPlayer.Play();
    }
}
