using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireFly : MonoBehaviour
{
    public float circleR; // 반지름
    public float deg; // 각도
    private float degSum;
    public float objSpeed; // 원운동 속도
    public float RBspeed = 0.05f;

    public bool fly = false;

    public Rigidbody2D fireflyRB;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(407.78f, -82.29f, 4.0f);
        fireflyRB.velocity = RBspeed * new Vector2(1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            deg += Time.deltaTime * objSpeed;
            degSum += Time.deltaTime * objSpeed;
            if (deg < 360)
            {
                var rad = -Mathf.Deg2Rad * (deg);
                var x = circleR * Mathf.Sin(rad);
                var y = circleR * Mathf.Cos(rad);
                transform.position = transform.position + new Vector3(x + RBspeed, y);
            }
            else
            {
                deg = 0;
            }
        }

        if(degSum >= 540.0f)
        {
            degSum = 545.0f;
            transform.position = new Vector3(transform.position.x, transform.position.y, 12.0f);
        }
        
    }
}
