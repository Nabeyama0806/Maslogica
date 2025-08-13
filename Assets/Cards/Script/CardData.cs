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
}