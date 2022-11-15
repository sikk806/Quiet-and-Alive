using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zeroPage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitforsec());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator waitforsec()
    {
        
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Main");
    }
}
