using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    //[SerializeField] private GameObject leftPlayerPrefab, rightPlayerPrefab, pianoPrefab;

    [SerializeField] private Vector2 leftPlayerSpawnOffset, rightPlayerSpawnOffset, pianoSpawnOffset;
    [SerializeField] private Transform leftPlayerTransform, rightPlayerTransform;
    [SerializeField] private Piano piano;

    [SerializeField] private LandingTrigger firstCheckPoint;
    private LandingTrigger lastCheckpoint; // maybe update this through an event or just set from Landing Trigger or a player

    private int points = 0;
    private float timePassed = 0;
    public int SecondsPassed { get => Mathf.FloorToInt(timePassed) % 60; }
    public int MinutesPassed { get => Mathf.FloorToInt(timePassed) / 60; }
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
        //Respawn();
        // starta tidtagning
        timePassed = 0;
        // reset poäng
        points = 0;
    }

    private void Respawn()
    {
        Vector2 leftPos = new Vector2();
        leftPos.x = lastCheckpoint.transform.position.x;
        leftPos.y = lastCheckpoint.transform.position.y;
        leftPos += leftPlayerSpawnOffset;
        leftPlayerTransform.position = leftPos;
        leftPlayerTransform.rotation = Quaternion.identity;

        Vector2 rightPos = new Vector2();
        rightPos.x = lastCheckpoint.transform.position.x;
        rightPos.y = lastCheckpoint.transform.rotation.y;
        rightPos += rightPlayerSpawnOffset;
        rightPlayerTransform.position = rightPos;
        rightPlayerTransform.rotation = Quaternion.identity;

        Vector2 pianoPos = new Vector2();
        pianoPos.x = lastCheckpoint.transform.position.x;
        pianoPos.y = lastCheckpoint.transform.position.y;
        pianoPos += pianoSpawnOffset;
        piano.ResetPiano(pianoPos);
        // score penalty for dropping piano?
    }

    public void EndGame()
    {
        piano.Unsubscribe(PianoShouldReset);
        IsGameRunning = false;
        // show some kind of end screen using the score & time
    }
}
