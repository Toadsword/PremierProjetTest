using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
