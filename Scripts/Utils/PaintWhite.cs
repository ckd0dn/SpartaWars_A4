using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintWhite : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderer;
    public Material flashWhiteMaterial;
    bool isFlash = false;
    private Coroutine flashWhiteCor;

    private void Awake()
    {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();

        foreach (var item in spriteRenderer)
        {
            item.material = flashWhiteMaterial;
        }
    }

    public void FlashWhite()
    {
        flashWhiteCor = StartCoroutine(FlashWhiteCoroutine());
    }

    private IEnumerator FlashWhiteCoroutine()
    {    
            foreach (var item in spriteRenderer)
            {
                item.material.SetFloat("_FlashAmount", 0.8f);
            }
       
            yield return new WaitForSeconds(0.1f);

            foreach (var item in spriteRenderer)
            {
                item.material.SetFloat("_FlashAmount", 0);

            }

            StopCoroutine(flashWhiteCor);
    }
}
