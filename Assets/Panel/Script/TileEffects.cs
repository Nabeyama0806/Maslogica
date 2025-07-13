using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//メモ : switch移植をする場合はObjectPoolを要検討

public class TileEffects : MonoBehaviour
{
    //Typeとエフェクトを紐づけるためのクラス
    [Serializable]
    public class Effects
    {
        public Type type;                   //自身がどのエフェクトか
        public GameObject effectPrefab;     //エフェクト
        public float destroyTime;           //削除する時間    
    }

    [SerializeField]  List<Effects> m_effects2DList;

    public enum Type
    { 
        Passive,        //非アクティブ
        Active,         //アクティブ
        PlayerAttack,   //プレイヤーの攻撃
        EnemyAttack,    //敵の攻撃
    }

    public void Play(Type type)
    {
        switch(type)
        {
            case Type.Passive:
                PlayPassive();
                break;

            case Type.Active:
                PlayActive();
                break;

            case Type.PlayerAttack:
                PlayPlayerAttack();
                break;

            case Type.EnemyAttack:
                PlayEnemyAttack();
                break;

        }
    }

    private void PlayPassive()
    {
        //エフェクトの生成と削除
       GameObject effect =  Instantiate(
            m_effects2DList[(int)Type.Passive].effectPrefab,
            transform.position,
            Quaternion.identity
            );

        Destroy(effect, m_effects2DList[(int)Type.Passive].destroyTime);
    }

    private void PlayActive()
    {
        //エフェクトの生成と削除
        GameObject effect = Instantiate(
             m_effects2DList[(int)Type.Active].effectPrefab,
             transform.position,
             Quaternion.identity
             );

        Destroy(effect, m_effects2DList[(int)Type.Active].destroyTime);
    }

    private void PlayPlayerAttack()
    {
        //エフェクトの生成と削除
        GameObject effect = Instantiate(
             m_effects2DList[(int)Type.PlayerAttack].effectPrefab,
             transform.position,
             Quaternion.identity
             );

        Destroy(effect, m_effects2DList[(int)Type.Active].destroyTime);
    }

    private void PlayEnemyAttack()
    {
        //エフェクトの生成と削除
        GameObject effect = Instantiate(
             m_effects2DList[(int)Type.EnemyAttack].effectPrefab,
             transform.position,
             Quaternion.identity
             );

        Destroy(effect, m_effects2DList[(int)Type.EnemyAttack].destroyTime);
    }

}
