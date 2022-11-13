using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkBones : MonoBehaviour
{
    public GameObject Bone;
    bool timeCheck = false;
    bool sinking = true;
    float checkTime;

    Color BoneColor;
    SpriteRenderer BoneRenderer;
    // Start is called before the first frame update
    void Start()
    {
        BoneRenderer = Bone.GetComponent<SpriteRenderer>();
        BoneColor = BoneRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeCheck)
        {
            timeCheck = true;
            checkTime = Time.time;
        }
        if (Time.time - checkTime >= 0.01f)
        {
            if (BoneColor.a > 0 && sinking)
            {
                BoneColor.a -= 0.005f;
                BoneRenderer.color = BoneColor;
                timeCheck = false;
            }
            else if(BoneColor.a <= 0 && sinking)
            {
                Debug.Log("check");
                StartCoroutine(sink());
            }
            else if (BoneColor.a < 1 && !sinking)
            {
                BoneColor.a += 0.005f;
                BoneRenderer.color = BoneColor;
                timeCheck = true;
            }
            else if(BoneColor.a >= 1 && !sinking)
            {
                sinking = true;
            }
        }
    }

    public IEnumerator sink()
    {
        Bone.SetActive(false);
        Debug.Log("check");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("check");
        sinking = false;
        Bone.SetActive(true);
    }
}
