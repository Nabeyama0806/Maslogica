using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    private List<CardController> m_cards;

    private void Start()
    {
        //カードを全て反転
        StartCoroutine(AllFlip());
    }

    private IEnumerator AllFlip()
    {
        //全てのカードを反転させる
        foreach (Transform card in transform)
        {
            yield return StartCoroutine(card.GetComponent<CardController>().Flip());
        }
    }
}
