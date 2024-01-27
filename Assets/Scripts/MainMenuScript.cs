using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject quit;
    public GameObject credits;
    public GameObject how;
    public GameObject play;

    AudioSource audiosource;
    public AudioClip music;
    
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = music;
        audiosource.Play();
        quit.SetActive(false);
        credits.SetActive(false);
        how.SetActive(false);
        play.SetActive(false);
    }

    public void PlayGame()
    {
        menu.SetActive(false);
        play.SetActive(true);
        Invoke("StartGame", 3);
    }

    private void StartGame()
    {
        SceneManager.LoadSceneAsync("Philip");
    }

    public void HowTo()
    {
        menu.SetActive(false);
        how.SetActive(true);
        Invoke("Instructions", 3);
    }

    private void Instructions()
    {
        // Show instructions
    }

    public void Credits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
        Invoke("AboutGame", 3);
    }

    private void AboutGame()
    {
        // Show credits
    }

    public void ExitGame()
    {
        menu.SetActive(false);
        quit.SetActive(true);
        Invoke("Quitting", 3);
    }

    private void Quitting()
    {
        Application.Quit();
    }
}
