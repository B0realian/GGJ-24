using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    //[SerializeField] private GameObject leftPlayerPrefab, rightPlayerPrefab, pianoPrefab;

    [SerializeField] private Vector3 leftPlayerSpawnOffset, rightPlayerSpawnOffset, pianoSpawnOffset;
    [SerializeField] private Transform leftPlayerTransform, rightPlayerTransform, pianoTransform;
    [SerializeField] private Piano piano;

    [SerializeField] private LandingTrigger firstCheckPoint;
    private LandingTrigger lastCheckpoint;

    private int points = 0;
    private float timePassed = 0;
    public int SecondsPassed { get => Mathf.FloorToInt(timePassed); }

    public bool IsGameRunning { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // har väl prefabsen i scenen från början
        //Vector3 spawnPosition = lastCheckpoint.transform.position;
        //Instantiate(leftPlayerPrefab, spawnPosition + leftPlayerSpawnOffset, Quaternion.identity);
        //Instantiate(rightPlayerPrefab, spawnPosition + rightPlayerSpawnOffset, Quaternion.identity);
        //Instantiate(pianoPrefab, spawnPosition + pianoSpawnOffset, Quaternion.identity);
        
        NewGame();
    }

    // Update is called once per frame
    private void Update()
    {
        timePassed += Time.unscaledDeltaTime;
    }

    private void PianoShouldReset(Piano piano)
    {
        Respawn();
    }

    public void NewGame()
    {
        piano.Subscribe(PianoShouldReset);
        IsGameRunning = true;
        // sätt last checkpoint till toppen
        lastCheckpoint = firstCheckPoint;
        // spawna piano & co där
        Respawn();
        // starta tidtagning
        timePassed = 0;
        // reset poäng
        points = 0;
    }

    private void Respawn()
    {
        leftPlayerTransform.position = lastCheckpoint.transform.position + leftPlayerSpawnOffset;
        leftPlayerTransform.rotation = Quaternion.identity;

        rightPlayerTransform.position = lastCheckpoint.transform.position + rightPlayerSpawnOffset;
        rightPlayerTransform.rotation = Quaternion.identity;

        pianoTransform.position = lastCheckpoint.transform.position + pianoSpawnOffset;
        pianoTransform.rotation = Quaternion.identity;

        // score penalty for dropping piano?
    }

    public void EndGame()
    {
        piano.Unsubscribe(PianoShouldReset);
        IsGameRunning = false;
        // show some kind of end screen using the score & time
    }
}
