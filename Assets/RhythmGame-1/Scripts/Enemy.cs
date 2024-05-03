using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    public GameObject Player;

    private Renderer renderer;

    public int _distance;
    public int _defenseMeter;
    public int _defenseMax;

    //Duration of enemy attack
    public float _enemyAttackWaitTime;
    //Duration of enemy block
    public float _enemyBlockingTime;

    //Duration of a beat
    float _beat = 6 / 19f;

    //Countdown for duration of enemy to begin blocking
    public float _enemyBlockWarning = 0;

    //Countdown for duration of enemy attack for player to block
    public float _attackTime;

    //Audio Source for audio cues
    public AudioSource _audio;

    public AudioClip _attackSFX;
    public AudioClip _defendSFX;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();

        _distance = 0;
        _defenseMeter = 0;
        _enemyAttackWaitTime = 12/19;
        _enemyBlockingTime = -1f;

        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Duration of enemy blocking
        if (_enemyBlockingTime >= -1)
        {
            _enemyBlockingTime -= Time.deltaTime;
        }

        //Incrementation of enemy block warning
        if (_enemyBlockWarning >= -1)
        {
            _enemyBlockWarning -= Time.deltaTime;
        }
        //Enemy begins blocking
        else if (_enemyBlockWarning <= 0)
        {
            _enemyBlockingTime = _beat;
        }

        //Incrementation of enemy attack
        if (_attackTime >= 0)
        {
            _attackTime -= Time.deltaTime; 
            if (_attackTime <= 0)
            {
                Player.GetComponent<Player>()._position -= 1f;
            }
        }
    }

    public void _attack()
    {
        _attackTime = _beat;

        renderer.material.color = new Color(255, 0, 0);
        _audio.clip = _attackSFX;
        _audio.Play();
    }
    public void _block()
    {
        _enemyBlockWarning = _beat;

        renderer.material.color = new Color(0, 0, 255);
        _audio.clip = _defendSFX;
        _audio.Play();
    }

    public void _wait()
    {
        
        renderer.material.color = new Color(0, 255, 0);
    }
}
