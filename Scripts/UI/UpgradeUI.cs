using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour, ISectable
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI goldTxt;
    public GoldUI goldUI;
    public Slot[] slots;
    public Transform upgradeSlots;
    


    public Slot activeSlot;

    private void Start()
    {

        slots = new Slot[upgradeSlots.childCount];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = upgradeSlots.GetChild(i).GetComponent<Slot>();
            slots[i].index = i;
        }

        SelectSlot(slots[0]);
    }

    public void SelectSlot(Slot selectedSlot)
    {

        foreach (Slot slot in slots)
        {
            if (slot == selectedSlot)
            {
                slot.Select();
                nameTxt.text = slot.slotData.slotName;
                levelTxt.text = slot.slotData.level.ToString();
                goldTxt.text = slot.slotData.gold.ToString();

                activeSlot = selectedSlot;
            }
            else
            {
                slot.Deselect();
            }
        }
    }

    public void Upgrade()
    {
        // 강화하기 버튼시 호출
        if ( DataManager.Instance.userData.Gold < activeSlot.slotData.gold)
        {
            UIManager.Instance.Show("골드가 부족합니다.");
        }
        else
        {
            DataManager.Instance.userData.Gold -= activeSlot.slotData.gold;

            activeSlot.slotData.level++;
            activeSlot.slotData.gold *= 2;

            DataManager.Instance.UpgradeApply();

            //DataManager.Instance.SaveData();
            //DataManager.Instance.LoadData();

            activeSlot.Select();
            nameTxt.text = activeSlot.slotData.slotName;
            levelTxt.text = activeSlot.slotData.level.ToString();
            goldTxt.text = activeSlot.slotData.gold.ToString();
            goldUI.UpdateGoldUI();
        }
        
        

    }


}
