using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageText : MonoBehaviour
{
    public TextMeshProUGUI stageText;

    // Start is called before the first frame update
    void Start()
    {
        stageText.text = $"�������� : {DataManager.Instance.selectStage}";
    }

}
