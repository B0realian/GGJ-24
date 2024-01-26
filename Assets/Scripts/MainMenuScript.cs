using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Philip");
    }

  
    public void AboutGame()
    {
        SceneManager.LoadSceneAsync("TeamAwesome");
    }

    public void ExitGame()
    {

    }
}
