using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UnitSelectionKTH : MonoBehaviour, ISectable
{
    private Slot choiceSlot;
    public Slot[] unitSlots;
    public Slot[] arrageSlots;

    public Transform unitSlotsParent;
    public Transform arrageSlotsParent;

    public event Action OnArrange;
    public event Action OnDelete;

    public GameObject startBtn;

    // Start is called before the first frame update
    void Start()
    {
        // ����, ��ġ ���� �ʱ�ȭ
        unitSlots = new Slot[unitSlotsParent.childCount];
        arrageSlots = new Slot[arrageSlotsParent.childCount];

        for (int i = 0; i < unitSlots.Length; i++)
        {
            unitSlots[i] = unitSlotsParent.GetChild(i).GetComponent<Slot>();
            unitSlots[i].index = i;
        }

        for (int i = 0; i < arrageSlots.Length; i++)
        {
            arrageSlots[i] = arrageSlotsParent.GetChild(i).GetComponent<Slot>();
            arrageSlots[i].index = i;
        }

        OnArrange += ArrangeUnit;
        OnArrange += checkReadyStart;
        OnArrange += CheckUnitButtons;

        OnDelete += DeleteUnit;
        OnDelete += checkReadyStart;
    }

    public void SelectSlot(Slot selectedSlot)
    {
        foreach (Slot slot in unitSlots)
        {
            if (slot == selectedSlot)
            {
                slot.Select();
                choiceSlot = slot;
            }
            else
            {
                slot.Deselect();
            }
        }
    }

    public void clickArrangeBtn()
    {
        OnArrange?.Invoke();
    }

    public void clickDeleteBtn()
    {
        OnDelete?.Invoke();
    }


    public void ArrangeUnit()
    {
        // ������ ������ ��ġ���Կ� �߰����ش�
        if (choiceSlot != null)
        {
            for (int i = 0; i < arrageSlots.Length; i++)
            {
                // ������ ����ְų� ������ ������ ���°�쿡�� �߰�
                if (arrageSlots[i].slotData == null && !checkEqualUnit(arrageSlots, choiceSlot))
                {
                    arrageSlots[i].slotData = choiceSlot.slotData;
                    arrageSlots[i].Set();
                    return;
                }
            }
        }
    }

    public void DeleteUnit()
    {
        for (int i = arrageSlots.Length - 1; i > -1; i--)
        {
            if (arrageSlots[i].slotData != null)
            {
                arrageSlots[i].Clear();
                return;
            }
        }
    }

    // ������ �������� Ȯ��
    public bool checkEqualUnit(Slot[] slots, Slot choiceSlot)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (arrageSlots[i].slotData != null)
            {
                if (arrageSlots[i].slotData.Equals(choiceSlot.slotData))
                {
                    return true;
                }
            }
        }
        return false;
    }


    // ��ġ�� �Ϸ�Ǿ����� Ȯ��
    public void checkReadyStart()
    {
        bool isFilledSlot = true;

        foreach (Slot slot in arrageSlots)
        {
            if (slot.slotData == null)
            {
                isFilledSlot = false;
                break;
            }
        }

        if (isFilledSlot)
        {
            startBtn.SetActive(true);
        }
        else
        {
            startBtn.SetActive(false);
        }
    }

    // ���ο� �޼���: ��ġ�� ������ �����ϰ� ��ư�� Ȱ��ȭ
    public void CheckUnitButtons()
    {
        // UnitButtonSlot �迭�� �����ͼ� ��
        UnitButtonSlot[] unitButtonSlots = FindObjectsOfType<UnitButtonSlot>();
        foreach (var buttonSlot in unitButtonSlots)
        {
            buttonSlot.UpdateButtonStates(arrageSlots);
        }
    }
}
