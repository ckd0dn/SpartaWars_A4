using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageBtn : MonoBehaviour
{
    public void NextStage()
    {
        DataManager.Instance.selectStage++;
        SceneManager.LoadScene("GameScene");
    }
    

}
