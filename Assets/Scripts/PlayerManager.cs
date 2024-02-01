using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public PlayerInputManager inputManager;
    [SerializeField] GameObject playerLeftPrefab;
    [SerializeField] GameObject playerRightPrefab;
    [SerializeField] GameObject pianoPrefab;
    [HideInInspector] public PlayerInput playerLeft;
    [HideInInspector] public PlayerInput playerRight;
    public Transform playerLeftSpawn;
    public Transform playerRightSpawn;
    public Transform pianoSpawn;
    public TextMeshProUGUI infoText;
    public AudioClip startClip01;
    public AudioClip startClip02;
    public AudioClip startClip03;
    AudioSource audioSource;
    private int _gamepads;

    void Awake()
    {
        infoText.enabled = false;
        inputManager = GetComponent<PlayerInputManager>();
        audioSource = GetComponent<AudioSource>();
        _gamepads = Gamepad.all.Count;
        
        SpawnPlayers();
    }

    public void SpawnPlayers()
    {
        playerLeftPrefab.transform.position = playerLeftSpawn.position;
        playerRightPrefab.transform.position = playerRightSpawn.position;

        switch (_gamepads)
        {
            case 0:
                infoText.enabled = true;
                infoText.text = "This game requires at least one gamepad preferably two";
                Invoke("NotEnoughGamepads", 5);
                break;
            case 1:
                playerLeft = PlayerInput.Instantiate(playerLeftPrefab, controlScheme: "GamePad", pairWithDevice: Gamepad.all[0]);
                playerRight = PlayerInput.Instantiate(playerRightPrefab, controlScheme: "Keyboard&Mouse", pairWithDevice: Keyboard.current);
                break;
            case > 1:
                playerLeft = PlayerInput.Instantiate(playerLeftPrefab, controlScheme: "GamePad", pairWithDevice: Gamepad.all[0]);
                playerRight = PlayerInput.Instantiate(playerRightPrefab, controlScheme: "GamePad", pairWithDevice: Gamepad.all[1]);
                break;
            default: break;
        }
        //switch (Random.Range(0, 4))
        //{
        //    case 0:

        //}

        Invoke("SpawnPiano", 2);
    }

    private void SpawnPiano()
    {
        Instantiate(pianoPrefab, pianoSpawn.position, Quaternion.identity);
    }

    private void NotEnoughGamepads()
    {
        SceneManager.LoadScene(0);
    }
}
