using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(TBL_MOVE_CARD.CountEntities); // 3
        int count = TBL_MOVE_CARD.CountEntities;

        for (int i = 0; i < count; ++i)
        {
            TBL_MOVE_CARD data = TBL_MOVE_CARD.GetEntity(i);
            Debug.Log(data.Name);
            Debug.Log(data.Distance);
            Debug.Log(data.Comment);
        }

        TBL_MOVE_CARD.ForEachEntity((data) =>
        {
            Debug.Log(data.Name);
            Debug.Log(data.Distance);
            Debug.Log(data.Comment);
        });


        var data3st = TBL_MOVE_CARD.GetEntity(3);
        if (data3st == null)
        {
            Debug.LogError("3번째 데이터 NUll");
        }
    }
}
