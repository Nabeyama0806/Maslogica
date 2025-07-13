using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float MovingTime = 3.0f;
    private float m_movingTime;

    private void Start()
    {
        m_movingTime = MovingTime;
    }

    public bool Play()
    {
        m_movingTime -= Time.deltaTime;

        if(m_movingTime <= 0)
        {
            //次のターンの準備
            m_movingTime = MovingTime;

            //攻撃マスをランダムで選択
            TileGrid.RandomSelect();

            //ターン終了
            return false;
        }
        
        //行動中
        return true;
    }
}
