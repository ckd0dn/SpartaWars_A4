using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitButtonSlot : MonoBehaviour
{
    public Button[] unitButtons;
    public UnitData[] unitDatas; // ��ư���� ���� UnitData �迭

    void Start()
    {
        // �ʱ⿡�� ��� ��ư ��Ȱ��ȭ
        foreach (var button in unitButtons)
        {
            if(button != null)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    // ��ġ�� ������ Ȯ���Ͽ� ��ư Ȱ��ȭ
    public void UpdateButtonStates(Slot[] arrangedSlots)
    {
        foreach (var button in unitButtons)
        {
            UnitData buttonUnitData = unitDatas[Array.IndexOf(unitButtons, button)];
            bool isUnitPresent = arrangedSlots.Any(slot => slot.slotData != null 
            && slot.slotData is UnitSlotData slotUnitData && slotUnitData.unitData.ID == buttonUnitData.ID);
            if (button != null)
            {
                button.gameObject.SetActive(isUnitPresent);
            }
        }
    }
}
