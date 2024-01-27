using System.Collections;
using System.Collections.Generic;
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
    private int _gamepads;

    void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        _gamepads = Gamepad.all.Count;
        playerLeftPrefab.transform.position = playerLeftSpawn.position;
        playerRightPrefab.transform.position = playerRightSpawn.position;

        switch (_gamepads)
        {
            case 0:
                // Show text "You require at least one gamepad to play this game!"
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
    }

    private void NotEnoughGamepads()
    {
        SceneManager.LoadScene(0);
    }
}
