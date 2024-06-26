using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBtn : MonoBehaviour
{
    public void StartGameScene(int stageNumber)
    {
        DataManager.Instance.selectStage = stageNumber;

        SceneManager.LoadScene("GameScene");
    }
}
