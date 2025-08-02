using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    [Serializable]
    public class ShapeData  //形状に関する1マスのデータ
    {
        public Vector2 position;
        public bool isActive;
    }

    [SerializeField] int m_totalActiveAmount;       //形状に必要なマスの数
    [SerializeField] int m_cardId;                  //主キー
    [SerializeField] float m_attack;                //攻撃力
    [SerializeField] List<ShapeData> m_shapeData;   //自身の形状

    public int CardId
    {
        get { return m_cardId; }
    }

    public bool Check()
    {
        //起動しているマスの数と形状に必要なマスの数が一致していなければ何もしない
        //if(m_totalActiveAmount != 100) return false;

        //形状のチェックを行う
        int index = 0;
        for (int y = 0; y < 8; ++y)
        {
            for (int x = 0; x < 8; ++x)
            {
                //一つでも状態が違うタイルがあれば失敗
                if (TileGrid.IsTileActive(m_shapeData[index].position) != m_shapeData[index].isActive) return false;
                index++;
            }
        }

        //形状が一致したので成功
        return true;
    }
}
