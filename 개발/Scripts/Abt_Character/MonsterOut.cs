using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOut : MonoBehaviour
{
    bool doOnce = false;
    bool dodo = false;
    public GameObject Monster;
    float checkTime;
    Character GameCharacter;
    Mosnter1F FallingMonster;
    // Start is called before the first frame update
    void Start()
    {
        GameCharacter = GameObject.Find("Character").GetComponent<Character>();
        FallingMonster = Monster.GetComponent<Mosnter1F>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameCharacter.portalTwice && !doOnce)
        {
            if (!dodo)
            {
                Debug.Log("Character Enter Lib1F : " + Time.time);
                dodo = true;
            }
            doOnce = true;
            StartCoroutine(MonOut());
        }
    }

    private IEnumerator MonOut()
    {
        yield return new WaitForSeconds(2.0f);
        Debug.Log("SetActive : " + Time.time);
        Monster.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        FallingMonster.check = true;

        //stop = false;
    }
}
