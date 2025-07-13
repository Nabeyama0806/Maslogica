using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileEffects : MonoBehaviour
{
    [Serializable]
    public class Effects           //Typeとエフェクトを紐づけるためのクラス
    {
        public EffectType type;    //自身がどのエフェクトか
        public GameObject effect;  
        public float destroyTime;  //削除する時間    
    }

    [SerializeField]  List<Effects> m_effectList;

    public enum EffectType
    { 
        Active,         //アクティブ
        PlayerAttack,   //プレイヤーの攻撃
        EnemyAttack,    //敵の攻撃
    }

    //エフェクトの表示
    public void Show(EffectType type, bool active = false)
    {
        switch (type)
        {
            case EffectType.Active:
                PlayActive(active);
                break;

            case EffectType.PlayerAttack:
                PlayPlayerAttack();
                break;

            case EffectType.EnemyAttack:
                PlayEnemyAttack();
                break;
        }
    }

    //エフェクトを非表示にする
    public void Inactive()
    {
        PlayActive(false);
    }

    private void PlayActive(bool active)
    {
        //エフェクトの表示
        m_effectList[(int)EffectType.Active].effect.SetActive(active);
    }

    private void PlayPlayerAttack()
    {
        //エフェクトの生成と削除
        GameObject effect = Instantiate(
             m_effectList[(int)EffectType.PlayerAttack].effect,
             transform.position,
             Quaternion.identity
             );
        
        Destroy(effect, m_effectList[(int)EffectType.PlayerAttack].destroyTime);

        //エフェクトを表示
        effect.SetActive(true);
    }

    private void PlayEnemyAttack()
    {
        //エフェクトの表示
        m_effectList[(int)EffectType.EnemyAttack].effect.SetActive(true);
    }
}
