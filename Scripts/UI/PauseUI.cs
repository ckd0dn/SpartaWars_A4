using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameObject Panel;

    public void OpenPanel()
    {
        if(Panel != null)
        {
            Panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Continue()
    {
        if(Panel != null)
        {
            Panel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
