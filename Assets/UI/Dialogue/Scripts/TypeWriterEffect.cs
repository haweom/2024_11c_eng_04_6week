using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float typeSpeed;
    
    public Coroutine Run(string textToType, TMP_Text text)
    {
        return StartCoroutine(TypeText(textToType, text));
    }

    private IEnumerator TypeText(string textToType, TMP_Text text)
    {
        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
            
            text.text = textToType.Substring(0, charIndex);
            
            yield return null;
        }
        text.text = textToType;
    }
}
