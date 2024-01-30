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
    public GameObject howImage;

    AudioSource audiosource;
    public AudioClip music;

    public float playDelay = 0.7f;
    
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = music;
        audiosource.Play();
        FalseAll();
        menu.SetActive(true);
    }

    public void PlayGame()
    {
        FalseAll();
        play.SetActive(true);
        Invoke("StartGame", playDelay);
    }

    private void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void HowTo()
    {
        FalseAll();
        how.SetActive(true);
        Invoke("Instructions", playDelay);
    }

    private void Instructions()
    {
        FalseAll();
        howImage.SetActive(true);
    }

    public void Credits()
    {
        FalseAll();
        credits.SetActive(true);
        Invoke("AboutGame", playDelay);
    }

    private void AboutGame()
    {
        // Show credits
    }

    public void ExitGame()
    {
        FalseAll();
        quit.SetActive(true);
        Invoke("Quitting", playDelay);
    }

    private void Quitting()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        FalseAll();
        menu.SetActive(true);
    }

    private void FalseAll()
    {
        menu.SetActive(false);
        quit.SetActive(false);
        credits.SetActive(false);
        how.SetActive(false);
        play.SetActive(false);
        howImage.SetActive(false);
    }

    }
