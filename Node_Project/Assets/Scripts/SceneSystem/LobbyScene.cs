using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();                    //부모 초기화
        Debug.Log("LobbyScene Init");//로비 씬 초기화
    }

    protected override void Clear()
    {
        Debug.Log("LobbyScene Clear");
    }

}