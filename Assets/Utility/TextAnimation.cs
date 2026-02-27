using System.Collections;
using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float typingSpeed = 0.05f;
    public float vibrationAmount = 1f;

    private Coroutine typingCoroutine;
    private Coroutine vibrationCoroutine;

    private string currentText;

    public void PlayText(string newText)
    {
        // ç°ìÆÇ¢ÇƒÇÈèàóùÇé~ÇﬂÇÈ
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        if (vibrationCoroutine != null) StopCoroutine(vibrationCoroutine);

        currentText = newText;
        textMesh.text = "";

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= currentText.Length; i++)
        {
            textMesh.text = currentText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }

        vibrationCoroutine = StartCoroutine(VibrateText());
    }

    IEnumerator VibrateText()
    {
        while (true)
        {
            textMesh.ForceMeshUpdate();
            var textInfo = textMesh.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                Vector3 offset = new Vector3(
                    Random.Range(-vibrationAmount, vibrationAmount),
                    Random.Range(-vibrationAmount, vibrationAmount),
                    0
                );

                for (int j = 0; j < 4; j++)
                {
                    vertices[vertexIndex + j] += offset;
                }
            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                textMesh.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            yield return null;
        }
    }
}