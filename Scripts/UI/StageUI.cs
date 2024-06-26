using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public int bestUnlockStage;

    public Button[] stageButtons;

    private void Awake()
    {
        stageButtons = GetComponentsInChildren<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        LevelCheck();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LevelCheck();
        }
    }

    public void LevelCheck()
    {
        bestUnlockStage = DataManager.Instance.userData.BestUnlockStage;          

        for (int i = 0; i < stageButtons.Length; i++)
        {
            if (bestUnlockStage <= i)
                stageButtons[i].interactable = false; //레벨 버튼 비활성화
            else
                stageButtons[i].interactable = true; //레벨 버튼 활성화
        }
    }
}
