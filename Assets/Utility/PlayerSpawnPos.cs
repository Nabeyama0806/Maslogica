using UnityEngine;

public class PlayerSpawnPos : MonoBehaviour
{
    private void OnEnable()
    {
        //CharacterControllerを有効のままにすると、内部の状態がリセットされず、正しく移動しなかった

        //プレイヤーを取得
        GameObject player = GetObject.Instance.Player;

        //CharacterControllerを無効化して内部状態をリセット
        CharacterController characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;

        //プレイヤーの座標を設定
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;

        //CharacterControllerを再有効化
        characterController.enabled = true;
    }
}