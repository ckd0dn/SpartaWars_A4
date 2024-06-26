using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public SlotData slotData;
    public Image Bg;
    public bool isSelected = false;
    public int index;
    public Image slotImg;

    private void Awake()
    {
        Bg = GetComponent<Image>();
    }

    private void Start()
    {
        Set();
    }

    public void Select()
    {
        isSelected = true;
        Bg.color = Color.green;
    }

    public void Deselect()
    {
        isSelected = false;
        Bg.color = Color.white; // 기본 색상으로 되돌립니다.
    }

    public void Set()
    {
        if (slotData != null)
        {
            slotImg.enabled = true;
            slotImg.sprite = slotData.image;
        }
    }

    public void Clear()
    {
        slotImg.enabled = false;
        slotData = null;
        slotImg.sprite = null;
    }
}
