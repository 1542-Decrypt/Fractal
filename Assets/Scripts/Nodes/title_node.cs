/*
* Source: https://forum.unity.com/threads/have-words-fade-in-one-by-one.525175/
*/
// I have respectfully taken this code bc I dont want to write my own. All credits to the OG coder.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using System.Threading.Tasks;

public class title_node : MonoBehaviour
{
    enum ShowType { WordByWord, FadeInOut }
    [SerializeField] ShowType ShowingMethod;
    public int HoldTime;
    [SerializeField] private TextMeshProUGUI m_TextComponent;
    [SerializeField] private float FadeSpeed = 20.0f;
    [SerializeField] private int RolloverCharacterSpread = 10;


    public void ShowText()
    {
        if (ShowingMethod == ShowType.WordByWord) {
            StartCoroutine(TypeInText());
        }
        else if (ShowingMethod == ShowType.FadeInOut)
        {
            FadeInOut();
        }
    }

    async void FadeInOut()
    {
        Animation textAnim = m_TextComponent.gameObject.GetComponent<Animation>();
        textAnim.Play("text_fade_in");
        await Task.Delay(HoldTime * 1000);
        textAnim.Play("text_fade_out");
    }
    /// <summary>
    /// Method to animate (fade in) vertex colors of a TMP Text object.
    /// </summary>
    /// <returns></returns>
    IEnumerator TypeInText()
    {
        // Set the whole text transparent
        m_TextComponent.color = new Color
            (
                m_TextComponent.color.r,
                m_TextComponent.color.g,
                m_TextComponent.color.b,
                0
            );
        // Need to force the text object to be generated so we have valid data to work with right from the start.
        m_TextComponent.ForceMeshUpdate();


        TMP_TextInfo textInfo = m_TextComponent.textInfo;
        Color32[] newVertexColors;

        int currentCharacter = 0;
        int startingCharacterRange = currentCharacter;
        bool isRangeMax = false;

        while (!isRangeMax)
        {
            int characterCount = textInfo.characterCount;

            // Spread should not exceed the number of characters.
            byte fadeSteps = (byte)Mathf.Max(1, 255 / RolloverCharacterSpread);

            for (int i = startingCharacterRange; i < currentCharacter + 1; i++)
            {
                // Skip characters that are not visible (like white spaces)
                if (!textInfo.characterInfo[i].isVisible) continue;

                // Get the index of the material used by the current character.
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                // Get the vertex colors of the mesh used by this text element (character or sprite).
                newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                // Get the index of the first vertex used by this text element.
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                // Get the current character's alpha value.
                byte alpha = (byte)Mathf.Clamp(newVertexColors[vertexIndex + 0].a + fadeSteps, 0, 87);

                // Set new alpha values.
                newVertexColors[vertexIndex + 0].a = alpha;
                newVertexColors[vertexIndex + 1].a = alpha;
                newVertexColors[vertexIndex + 2].a = alpha;
                newVertexColors[vertexIndex + 3].a = alpha;

                if (alpha == 87)
                {
                    startingCharacterRange += 1;

                    if (startingCharacterRange == characterCount)
                    {
                        // Update mesh vertex data one last time.
                        m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                        yield return new WaitForSeconds(1.0f);

                        // fade out
                        m_TextComponent.gameObject.GetComponent<Animation>().Play("text_fade_out");

                        yield return new WaitForSeconds(1.0f);

                        // Reset our counters.
                        currentCharacter = 0;
                        startingCharacterRange = 0;
                        isRangeMax = true; // Would end the coroutine.
                    }
                }
            }

            // Upload the changed vertex colors to the Mesh.
            m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            if (currentCharacter + 1 < characterCount) currentCharacter += 1;

            yield return new WaitForSeconds(0.25f - FadeSpeed * 0.01f);
        }
    }
}
