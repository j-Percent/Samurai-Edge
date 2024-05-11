using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public AK.Wwise.Event _attackCue;
    public AK.Wwise.Event _blockCue;
    public bool _enemyEnter;

    public GameObject Player;

    //private Renderer renderer;

    public float _xStart;
    public float _yStart;

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

    //Sprites
    public Sprite _enemyStatic;
    public Sprite _enemyAttackCue;
    public Sprite _enemyAttack;
    public Sprite _enemyDefendCue;
    public Sprite _enemyDefend;
    public Sprite _enemyBlocked;
    public Sprite _enemyHit;
    public Sprite _enemyDefeat;
    public Sprite _enemyDeath;

    // Start is called before the first frame update
    void Start()
    {
        _enemyEnter = false;
        _xStart = 0.5f;
        _yStart = -0.695f;

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
        if (!Player.GetComponent<Player>()._gameStart && _enemyEnter)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, new Vector2(_xStart, _yStart), 0.02f);
        }

        Vector3 newPosition = gameObject.transform.position;
        newPosition.x = Player.GetComponent<Player>()._position + 1f;
        if (Player.GetComponent<Player>()._gameStart)
            gameObject.transform.position = Vector2.Lerp(this.transform.position, newPosition, 0.02f);

        //Debug.Log(_defenseMeter);
        //Duration of enemy blocking
        if (_enemyBlockingTime >= 0)
        {
            _enemyBlockingTime -= Time.deltaTime;
            if (_enemyBlockingTime <= 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = _enemyStatic;
            }
        }

        //Incrementation of enemy block warning
        if (_enemyBlockWarning >= 0)
        {
            _enemyBlockWarning -= Time.deltaTime;
            //Enemy begins blocking
            if (_enemyBlockWarning <= 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = _enemyDefendCue;
                _enemyBlockingTime = _beat;
            }
        }
       
        //Incrementation of enemy attack
        if (_attackTime >= 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = _enemyAttackCue;
            _attackTime -= Time.deltaTime;
            if (_attackTime <= 0 && _attackTime > -1)
            {
                this.GetComponent<SpriteRenderer>().sprite = _enemyAttack;
                Player.GetComponent<Player>()._cd = _beat;
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plHit;
                Player.GetComponent<Player>()._position -= 0.2f;
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
        if (_cd <= 0 && !Player.GetComponent<Player>()._gameOver) {
            _attackCue.Post(gameObject);
            _attackTime = _beat;

            //renderer.material.color = new Color(255, 0, 0);
            _audio.clip = _attackSFX;
            _audio.Play();
        }
    }
    public void _block()
    {
        if (_cd <= 0 && !Player.GetComponent<Player>()._gameOver)
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
        if (_cd <= 0 && !Player.GetComponent<Player>()._gameOver)
        {
            this.GetComponent<SpriteRenderer>().sprite = _enemyStatic;
        }
    }
}
