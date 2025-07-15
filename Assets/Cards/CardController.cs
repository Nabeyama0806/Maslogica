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
    private bool m_isFront;

    private void Awake()
    {
        m_image = GetComponent<Image>();
        m_rectTransform = GetComponent<RectTransform>();
        m_isFront = false;
    }

    private void OnEnable()
    {
        m_image.sprite = m_cardBack;
    }

    public IEnumerator Flip()
    {
        float tick = 0;
        Vector3 startRotate = new Vector3(0, 0, 0);
        Vector3 endRotate = new Vector3(0, 90, 0);
        Vector3 rotate;

        //最初から中間地点までひっくり返す
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * m_flipSpeed;

            rotate = Vector3.Lerp(startRotate, endRotate, tick);

            m_rectTransform.rotation = Quaternion.Euler(rotate);

            yield return null;
        }

        //カードの画像を変更する
        m_image.sprite = m_cardFront;

        tick = 0f;

        //中間から最後までひっくり返す
        while (tick < 1.0f)
        {
            tick += Time.deltaTime * m_flipSpeed;

            rotate = Vector3.Lerp(endRotate, startRotate, tick);

            m_rectTransform.rotation = Quaternion.Euler(rotate);

            yield return null;
        }
    }
}
