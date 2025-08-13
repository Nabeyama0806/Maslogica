using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardFlip : MonoBehaviour
{
    [SerializeField] List<Sprite> m_cardFrontList;
    [SerializeField] Sprite m_cardBack;
    [SerializeField] float m_flipSpeed;

    private Image m_image;
    private int  m_cardFrontIndex;
    private RectTransform m_rectTransform;
    private bool m_isFront;

    private void Awake()
    {
        m_image = GetComponent<Image>();
        m_rectTransform = GetComponent<RectTransform>();

        //裏面から開始する
        m_isFront = false;
        m_image.sprite = m_cardBack;

        //表面をランダム選択
        m_cardFrontIndex = Random.Range(0, m_cardFrontList.Count);
    }

    private void OnDisable()
    {
        //裏面から開始する
        m_isFront = false;
        m_image.sprite = m_cardBack;

        //表面をランダム選択
        m_cardFrontIndex = Random.Range(0, m_cardFrontList.Count);

        //途中で非表示にした場合も角度を初期位置に戻す
        m_rectTransform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void OnClick()
    {
        StartCoroutine(Play());

        //盤面に選択された形状を表示する

    }

    public IEnumerator Play()
    {
        float tick = 0;
        Vector3 endRotate = new Vector3(0, 90, 0);
        Vector3 rotate;

        //最初から中間まで回転させる
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * m_flipSpeed;

            rotate = Vector3.Lerp(Vector3.zero, endRotate, tick);

            m_rectTransform.rotation = Quaternion.Euler(rotate);

            yield return null;
        }

        //カードの画像を変更する
        m_isFront = !m_isFront;
        m_image.sprite = m_isFront ? m_cardFrontList[m_cardFrontIndex] : m_cardBack;

        tick = 0f;

        //中間から最後まで最初から中間地点まで回転させる
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * m_flipSpeed;

            rotate = Vector3.Lerp(endRotate, Vector3.zero, tick);

            m_rectTransform.rotation = Quaternion.Euler(rotate);

            yield return null;
        }
    }
}
