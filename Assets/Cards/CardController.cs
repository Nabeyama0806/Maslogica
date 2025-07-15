using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] Sprite m_cardFront;
    [SerializeField] Sprite m_cardBack;
    [SerializeField] float m_flipSpeed;

    private Image m_image;
    private RectTransform m_rectTransform;

    private void Awake()
    {
        m_image = GetComponent<Image>();
        m_rectTransform = GetComponent<RectTransform>();

        //裏面から開始する
        m_image.sprite = m_cardBack;
    }

    private void OnDisable()
    {
        //裏面から開始する
        m_image.sprite = m_cardBack;

        //途中で非表示にした場合も角度を初期位置に戻す
        m_rectTransform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public IEnumerator Flip()
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
        m_image.sprite = m_cardFront;

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
