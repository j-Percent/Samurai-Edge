using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public AK.Wwise.Event _attackCue;
    public AK.Wwise.Event _blockCue;

    public GameObject Player;

    //private Renderer renderer;

    public int _distance;
    public int _defenseMeter;
    public int _defenseMax;

    //Duration of enemy attack
    public float _enemyAttackWaitTime;
    //Duration of enemy block
    public float _enemyBlockingTime;

    //Duration of a beat
    float _beat = 6 / 19f;

    //Cooldown for breakdown
    public float _cd = 0;

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
        //renderer = GetComponent<Renderer>();

        _distance = 0;
        _defenseMeter = 0;
        _defenseMax = 100;
        _enemyAttackWaitTime = 8/19;
        _enemyBlockingTime = -1f;

        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPosition = gameObject.transform.position;
        newPosition.x = Player.GetComponent<Player>()._position + 1.64f;
        gameObject.transform.position = newPosition;

        Debug.Log(_defenseMeter);
        //Duration of enemy blocking
        if (_enemyBlockingTime >= 0)
        {
            _enemyBlockingTime -= Time.deltaTime;
        }

        //Incrementation of enemy block warning
        if (_enemyBlockWarning >= 0)
        {
            _enemyBlockWarning -= Time.deltaTime;
            //Enemy begins blocking
            if (_enemyBlockWarning <= 0)
            {
                _enemyBlockingTime = _beat;
            }
        }
       
        //Incrementation of enemy attack
        if (_attackTime >= 0)
        {
            _attackTime -= Time.deltaTime; 
            if (_attackTime <= 0)
            {
                Player.GetComponent<Player>()._position -= 0.3f;
                _defenseMeter -= 10;
            }
        }




    #region defense meter over 100, break down
            if (_defenseMeter > _defenseMax)
            {
                _defenseMeter = 0;
                _cd = 1.5f;
                //renderer.material.color = new Color(0, 255, 0);
            }

            if (_defenseMeter <= 0)
            {
                _defenseMeter = 0;
            }

            if (_cd >= 0)
            {
                _cd -= Time.deltaTime;
            }
    #endregion


    }

    //All the _cd <= is for breakdown
    public void _attack()
    {
        if (_cd <= 0) {
            _attackCue.Post(gameObject);
            _attackTime = _beat;

            //renderer.material.color = new Color(255, 0, 0);
            _audio.clip = _attackSFX;
            _audio.Play();
        }
    }
    public void _block()
    {
        if (_cd <= 0)
        {
            _blockCue.Post(gameObject);
            _enemyBlockWarning = _beat;

           // renderer.material.color = new Color(0, 0, 255);
            _audio.clip = _defendSFX;
            _audio.Play();
        }
    }

    public void _wait()
    {
        if (_cd <= 0)
        {
            //renderer.material.color = new Color(0, 255, 0);
        }
    }
}
