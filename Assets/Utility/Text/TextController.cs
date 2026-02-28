using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    private TextView m_view;
    private List<string> m_textList;
    private int m_textIndex;

    public void Initialize(List<string> text)
    {
        m_view = transform.GetChild(0).gameObject.AddComponent<TextView>();
        m_textList = text;
        m_textIndex = 0;

        if (m_textList != null && m_textList.Count > 0) m_view.Play(m_textList[m_textIndex]);
    }

    public bool Advance()
    {
        if (m_view.State == TextState.Typing)
        {
            m_view.ForceComplete();
            return true;
        }

        if (m_view.State == TextState.Completed)
        {
            //次のテキストを表示
            m_textIndex++;
            if (m_textIndex >= m_textList.Count) return false;
            m_view.Play(m_textList[m_textIndex]);
        }

        return true;
    }
}