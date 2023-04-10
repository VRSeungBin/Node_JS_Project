using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public string sceneName;
    public int sceneIndex;
    public bool sceneDataShow;
    public string sceneData0, sceneData1;
    private string sceneType;

    public void Awake()
    {
        Init();
    }

    protected virtual void Init()           //virtual 선언 base 쓸수 있게 만듬
    {

    }

    protected virtual void Clear()           //Scene 종료시에 해제 해야 할 것들을 위한 함수
    {

    }


}
