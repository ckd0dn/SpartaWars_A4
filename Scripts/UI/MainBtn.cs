using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBtn : MonoBehaviour
{
    public void Main()
    {
        SceneManager.LoadScene("StartScene");
    }
}
