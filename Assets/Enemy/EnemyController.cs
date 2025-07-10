using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float MovingTime = 3.0f;
    private float m_moveingTime;

    private void Start()
    {
        m_moveingTime = MovingTime;
    }

    public bool IsPlay()
    {
        m_moveingTime -= Time.deltaTime;

        if(m_moveingTime <= 0)
        {
            //次のターンの準備
            m_moveingTime = MovingTime;

            //攻撃マスをランダムで選択
            TileGrid.RandomSelect();

            //ターン終了
            return false;
        }
        
        //行動中
        return true;
    }
}
