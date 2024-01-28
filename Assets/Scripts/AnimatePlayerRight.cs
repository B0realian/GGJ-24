using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerRight : MonoBehaviour
{
    public  PlayerScript _player;
    private Animator _animator;
    private AudioSource _audioSource;

    [Header("Audio")]
    public AudioClip idleSound;
    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip fallSound;

    PlayerState _currentState;

    private void Start()
    {
        _player = GetComponent<PlayerScript>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        GetCurrentState(_player.state);
        
    }

    private void GetCurrentState(PlayerState _newState)
    {

        if (_currentState == _newState) return;

        _currentState = _newState;

        switch (_currentState)
        {
            case PlayerState.Idle:
                _audioSource.clip = idleSound;
                _audioSource.Play();
                _animator.Play("Right_Idle");
                break;

            case PlayerState.Walking:
                _audioSource.clip = walkSound;
                _audioSource.Play();
                _animator.Play("Right_Walk");
                break;

            case PlayerState.Jumping:
                _audioSource.clip = jumpSound;
                _audioSource.Play();
                _animator.Play("Right_Jump");
                break;

            case PlayerState.Falling:
                _audioSource.clip = fallSound;
                _audioSource.Play();
                _animator.Play("Right_Idle");
                break;
            default: break;
        }
    }
}
