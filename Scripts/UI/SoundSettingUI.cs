using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettingUI : MonoBehaviour
{
    public void SoundPanelOpen()
    {
        SoundManager.Instance.SoundPanel.SetActive(true);
    }
}
