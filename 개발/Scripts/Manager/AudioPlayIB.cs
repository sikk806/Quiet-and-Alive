using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayIB : MonoBehaviour
{
    public AudioClip[] audClip;
    public AudioSource audSource;
    Character Character;
    bool doOnce = false;
    bool change = false;
    public int clipNO = 0;

    // Start is called before the first frame update
    void Start()
    {
        audSource.clip = audClip[0];
        Character = GameObject.Find("Character").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Character.anim.GetBool("JumpMap") && !change)
        {
            clipNO = 1;
            change = true;
            doOnce = false;
        }
        if (!doOnce)
        {
            audSource.clip = audClip[clipNO];
            audSource.Play();
            doOnce = true;
        }
    }
}
