using UnityEngine;

public class PlayerSpawnPos : MonoBehaviour
{
    private GameObject m_player;

    private void FixedUpdate()
    {
        //既にプレイヤーを取得していれば何もしない
        if (m_player) return;

        //ヒエラルキー上のプレイヤーを取得
        m_player = GameObject.FindGameObjectWithTag("Player");

        //指定した位置にプレイヤーを配置
        m_player.transform.position = transform.position;
    }
}
