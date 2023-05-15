using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;                      //InputSystem 네입스페이스 추가

public class PlayerInputSystem : MonoBehaviour
{
    public Vector3 inputVec;
    public float speed;                     //속도

    public Rigidbody rigid;                 //물리 이동

    private void FixedUpdate()
    {
        Vector3 nextVec = new Vector3(inputVec.x, 0.0f, inputVec.y);            //x,y 좌표를 (x,0,y)로 변환해서 3D 이동 대응
        rigid.MovePosition(rigid.position + nextVec * speed * Time.fixedDeltaTime);         

    }

    private void OnMove(InputValue value)           //InputValue 인수 값을 가져온다. 
    {
        inputVec = value.Get<Vector2>();            //Vector2값으로 인수 값을 가져온다. 
    }
}
