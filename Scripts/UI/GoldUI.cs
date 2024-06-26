using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    public TextMeshProUGUI goldUI;

    private void Awake()
    {
        UpdateGoldUI();
    }

    public void UpdateGoldUI()
    {
        goldUI.text = DataManager.Instance.userData.Gold.ToString();
    }
}
