/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    bool KeyDown = true;
    //�Ѿ��� �߻�� ��ġ
    public Transform pos;
    public Transform Target;

    //���� -> Center�� Target�� �ٶ󺸰� �����Ƿ�, Rotation�� �������� ó����
    //public Transform Center;

    //�Ѿ� ������Ʈ
    public GameObject bullet;
    void Start()
    {
        Target = GameObject.Find("Character").transform;
    }
    
    void Update()
    {
        var temp;
        var target_dir;
        if (KeyDown)
        {
            //�����̽��ٸ� ������
            if (Input.GetKeyDown(KeyCode.Space))
            {
                KeyDown = false;
                //�Ѿ� ����
                temp = Instantiate(bullet);
                Destroy(temp, 2f);

                //�Ѿ� ���� ��ġ�� ���� �Ա��� �Ѵ�.
                temp.transform.position = pos.position;

                //�Ѿ��� ������ Center�� �������� �Ѵ�.
                //->������ Center������Ʈ�� Target�� �ٶ󺸰� �����Ƿ�, Rotation�� ������ ��.
                //temp.transform.rotation = Center.rotation;
                //���� �Ѿ��� ��ġ���� �÷����� ��ġ�� ���Ͱ��� �y���Ͽ� ������ ����
                target_dir = Target.transform.position - temp.transform.position;

                //x,y�� ���� �����Ͽ� Z���� ������ ������. -> ~�� ������ ����
                var angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;

                //Target �������� �̵�
                temp.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        else
        {
            float x = temp.transfomr.position.x + 1000 * target_dir.x;
            float y = temp.transfomr.position.y + 1000 * target_dir.y;
            Vector2 ccc = new Vector2(x, y);
            bullet.transform.position = new Vector2.MoveTorwards(temp.transfomr.position, ccc, 0.1f);
        }
    }
}
*/