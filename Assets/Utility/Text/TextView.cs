using System.Collections;
using UnityEngine;
using TMPro;

public enum TextState
{
    Idle,
    Typing,
    Completed
}

public class TextView : MonoBehaviour
{
    [SerializeField] float typingSpeed = 0.05f;
    [SerializeField] float animationAmount = 0.3f;

    public TextState State { get; private set; } = TextState.Idle;

    private TextMeshProUGUI m_text;
    private string m_currentText;
    private Coroutine m_typingCoroutine;
    private Coroutine m_animationCoroutine;

    private void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }

    public void Play(string message)
    {
        StopAllCoroutines();
        m_currentText = message;
        m_typingCoroutine = StartCoroutine(Typing());
    }

    public void ForceComplete()
    {
        if (State != TextState.Typing) return;

        StopCoroutine(m_typingCoroutine);
        m_text.text = m_currentText;
        State = TextState.Completed;
        m_animationCoroutine = StartCoroutine(Animation());
    }

    private IEnumerator Typing()
    {
        State = TextState.Typing;
        m_text.text = "";

        for (int i = 0; i <= m_currentText.Length; i++)
        {
            m_text.text = m_currentText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }

        State = TextState.Completed;
        m_animationCoroutine = StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        while (State == TextState.Completed)
        {
            m_text.ForceMeshUpdate();
            var info = m_text.textInfo;

            for (int i = 0; i < info.characterCount; i++)
            {
                if (!info.characterInfo[i].isVisible) continue;

                int index = info.characterInfo[i].vertexIndex;
                int matIndex = info.characterInfo[i].materialReferenceIndex;

                Vector3[] vertices = info.meshInfo[matIndex].vertices;
                Vector3 offset = Random.insideUnitCircle * animationAmount;

                for (int j = 0; j < 4; j++)
                {
                    vertices[index + j] += offset;
                }
            }

            for (int i = 0; i < info.meshInfo.Length; i++)
            {
                info.meshInfo[i].mesh.vertices = info.meshInfo[i].vertices;
                m_text.UpdateGeometry(info.meshInfo[i].mesh, i);
            }

            yield return null;
        }
    }
}