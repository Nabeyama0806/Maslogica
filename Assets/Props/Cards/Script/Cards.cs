using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    private void OnEnable()
    {
        //カードを全て反転
        StartCoroutine(AllFlip());
    }

    private IEnumerator AllFlip()
    {
        //子オブジェクトが全てアクティブになるまで少し待機
        yield return new WaitForSeconds(0.1f);

        //全てのカードを反転させる
        foreach (Transform card in transform)
        {
            yield return StartCoroutine(card.GetComponent<CardFlip>().Play());
        }
    }
}
