using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public void LoadSinglePLayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }
    public void LoadCoopMode(){
        SceneManager.LoadScene(2);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
