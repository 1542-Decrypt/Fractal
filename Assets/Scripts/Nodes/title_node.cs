/*
* Source: https://forum.unity.com/threads/have-words-fade-in-one-by-one.525175/
*/
// I have respectfully taken parts of this code bc I dont want to write my own. Credits to the OG coder.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class title_node : MonoBehaviour
{
    public bool DoNotPlay;
    enum ShowType { WordByWord, FadeInOut }
    [Tooltip("Word by word - P2-esque, when each letter fades in and fades out as a whole title. FadeInOut - Title just fades in, holds and fades out.")]
    [SerializeField] ShowType ShowingMethod;
    public float FadeinTime, HoldTime, FadeoutTime;
    [Range(0f, 1f)]
    [Tooltip("Needed transparency value.")]
    public float NeededAlpha;
    [Tooltip("Title text object.")]
    public TextMeshProUGUI m_TextComponent;
    Mesh mesh;
    [SerializeField] private int RolloverCharacterSpread = 1;

    public void Disable()
    {
        DoNotPlay = true;
    }
    public void ShowText()
    {
        if (DoNotPlay)
        {
            m_TextComponent.color = new Color(m_TextComponent.color.r, m_TextComponent.color.g, m_TextComponent.color.b, 0f);
            return;
        }
        if (ShowingMethod == ShowType.WordByWord) {
            StartCoroutine(LetterByLetter());
        }
        else if (ShowingMethod == ShowType.FadeInOut)
        {
            StartCoroutine(FadeinOut());
        }
    }
    private IEnumerator FadeinOut()
    {
        float INduration = FadeinTime;
        float currentTime = 0f;
        while (currentTime < INduration)
        {
            float alpha = Mathf.Lerp(0f, NeededAlpha, currentTime / INduration);
            m_TextComponent.color = new Color(m_TextComponent.color.r, m_TextComponent.color.g, m_TextComponent.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        m_TextComponent.color = new Color(m_TextComponent.color.r, m_TextComponent.color.g, m_TextComponent.color.b, NeededAlpha);

        yield return new WaitForSeconds(HoldTime);

        float OUTduration = FadeoutTime;
        currentTime = 0f;
        while (currentTime < OUTduration)
        {
            float alpha = Mathf.Lerp(NeededAlpha, 0f, currentTime / OUTduration);
            m_TextComponent.color = new Color(m_TextComponent.color.r, m_TextComponent.color.g, m_TextComponent.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        m_TextComponent.color = new Color(m_TextComponent.color.r, m_TextComponent.color.g, m_TextComponent.color.b, 0f);
        yield break;
    }
    private IEnumerator LetterByLetter()
    {
        float currentTime = 0f;
        float NeededAlpha32 = NeededAlpha * 255;
        m_TextComponent.color = new Color
            (
                m_TextComponent.color.r,
                m_TextComponent.color.g,
                m_TextComponent.color.b,
                0
            );
        m_TextComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
        Color32[] newVertexColors;
        int currentCharacter = 0;
        int startingCharacterRange = currentCharacter;
        bool isRangeMax = false;
        while (!isRangeMax)
        {
            int characterCount = textInfo.characterCount;
            byte fadeSteps = (byte)Mathf.Max(1, NeededAlpha32 / RolloverCharacterSpread);
            for (int i = startingCharacterRange; i < currentCharacter + 1; i++)
            {
                if (!textInfo.characterInfo[i].isVisible) continue;
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                newVertexColors = textInfo.meshInfo[materialIndex].colors32;
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                byte alpha = (byte)Mathf.Clamp(newVertexColors[vertexIndex + 0].a + fadeSteps, 0, NeededAlpha32);

                newVertexColors[vertexIndex + 0].a = alpha;
                newVertexColors[vertexIndex + 1].a = alpha;
                newVertexColors[vertexIndex + 2].a = alpha;
                newVertexColors[vertexIndex + 3].a = alpha;

                if (alpha < NeededAlpha32 && alpha > NeededAlpha32-1)
                {
                    startingCharacterRange += 1;

                    if (startingCharacterRange == characterCount)
                    {
                        m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                        currentCharacter = 0;
                        startingCharacterRange = 0;
                        isRangeMax = true;
                    }
                }
            }
            m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            if (currentCharacter + 1 < characterCount) currentCharacter += 1;
            yield return new WaitForSeconds(0.25f - FadeinTime * 0.01f);
        }
        m_TextComponent.color = new Color(m_TextComponent.color.r, m_TextComponent.color.g, m_TextComponent.color.b, NeededAlpha);
        yield return new WaitForSeconds(HoldTime);
        float OUTduration = FadeoutTime;
        currentTime = 0f;
        while (currentTime < OUTduration)
        {
            float alpha = Mathf.Lerp(NeededAlpha, 0f, currentTime / OUTduration);
            m_TextComponent.color = new Color(m_TextComponent.color.r, m_TextComponent.color.g, m_TextComponent.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        m_TextComponent.color = new Color(m_TextComponent.color.r, m_TextComponent.color.g, m_TextComponent.color.b, 0f);
        yield break;
    }
}
