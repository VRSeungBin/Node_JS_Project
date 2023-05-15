using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;                                   //시스템 사용
using Random = UnityEngine.Random;              //랜덤 설정 

public static class ListExtentions 
{
    public static T RandomItem<T>(this List<T> list)
    {
        if (list.Count == 0)
            throw new IndexOutOfRangeException("List is Emty");

        var randomIndex = Random.Range(0, list.Count);

        return list[randomIndex];
    }
}
