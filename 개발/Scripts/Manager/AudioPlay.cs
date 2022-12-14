using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlay : MonoBehaviour
{
    public AudioClip[] audClip;
    public AudioSource audSource;
    Character Character;
    bool doOnce = false;
    bool change = false;

    // Start is called before the first frame update
    void Start()
    {
        audSource.clip = audClip[0];
        Character = GameObject.Find("Character").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Character.portalTwice && !change)
        {
            audSource.clip = audClip[1];
            doOnce = false;
            change = true;
        }
        if (!doOnce)
        {
            audSource.Play();
            doOnce = true;
        }
    }
}
