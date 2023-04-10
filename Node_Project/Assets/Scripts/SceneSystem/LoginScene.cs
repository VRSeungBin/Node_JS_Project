using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();                //부모 초기화
        Debug.Log("LoginScene Init");//로그인 씬 초기화
    }

    protected override void Clear()
    {        
        Debug.Log("LoginScene Clear");
    }

}
