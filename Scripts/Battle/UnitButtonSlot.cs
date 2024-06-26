using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitButtonSlot : MonoBehaviour
{
    public Button[] unitButtons;
    public UnitData[] unitDatas; // 버튼들이 가진 UnitData 배열

    void Start()
    {
        // 초기에는 모든 버튼 비활성화
        foreach (var button in unitButtons)
        {
            if(button != null)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    // 배치된 유닛을 확인하여 버튼 활성화
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
