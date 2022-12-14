using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    private HP HP;
    private GameOver GameOver;

    float checkTime = 0;

    private bool doOnce = false;
    public GameObject VideoPanel;
    public RawImage mScreen = null;
    public VideoPlayer mVideoPlayer = null;
    Color alpha;

    // Start is called before the first frame update
    void Start()
    {
        if(mScreen != null && mVideoPlayer != null)
        {
            StartCoroutine(PrepareVideo());
        }

        HP = GameObject.Find("HP").GetComponent<HP>();
        GameOver = GameObject.Find("GameOver").GetComponent<GameOver>();
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
        GameOver.Gameover = true;
    }

}
