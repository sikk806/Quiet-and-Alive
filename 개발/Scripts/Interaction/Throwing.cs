/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    bool KeyDown = true;
    //총알이 발사될 위치
    public Transform pos;
    public Transform Target;

    //방향 -> Center가 Target을 바라보고 있으므로, Rotation은 방향으로 처리함
    //public Transform Center;

    //총알 오브젝트
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
            //스페이스바를 누를시
            if (Input.GetKeyDown(KeyCode.Space))
            {
                KeyDown = false;
                //총알 생성
                temp = Instantiate(bullet);
                Destroy(temp, 2f);

                //총알 생성 위치를 머즐 입구로 한다.
                temp.transform.position = pos.position;

                //총알의 방향을 Center의 방향으로 한다.
                //->참조된 Center오브젝트가 Target을 바라보고 있으므로, Rotation이 방향이 됨.
                //temp.transform.rotation = Center.rotation;
                //현재 총알의 위치에서 플레이의 위치의 벡터값을 뻴셈하여 방향을 구함
                target_dir = Target.transform.position - temp.transform.position;

                //x,y의 값을 조합하여 Z방향 값으로 변형함. -> ~도 단위로 변형
                var angle = Mathf.Atan2(target_dir.y, target_dir.x) * Mathf.Rad2Deg;

                //Target 방향으로 이동
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