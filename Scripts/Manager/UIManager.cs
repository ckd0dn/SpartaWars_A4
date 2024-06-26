using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI textMeshProUGUI;

    IEnumerator enumerator;

    private void Start()
    {
        
    }

    public void Show(string text)
    {
        if (enumerator != null)
        {
            StopCoroutine(enumerator);
        }
        //StopCoroutine(enumerator);

        textMeshProUGUI.text = text;

        enumerator = FadeOut();
        StartCoroutine(enumerator);
    }

    public IEnumerator FadeOut()
    {        
        textMeshProUGUI.enabled = true;

        for (float f = 1f; f > 0; f -= 0.02f)
        {
            Color c = textMeshProUGUI.color;
            c.a = f;
            textMeshProUGUI.color = c;
            yield return new WaitForSeconds( 0.01f);
        }

        textMeshProUGUI.enabled = false;

    }

}
