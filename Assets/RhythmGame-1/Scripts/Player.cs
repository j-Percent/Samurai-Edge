using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private Renderer renderer;

    public float _xStart;
    public float _yStart;
    public bool _playerEnter;

    public bool _gameStart;
    public bool _gameOver;
    //Check player win
    public bool _win;
    //Check player loss
    public bool _lose;

    //Countdown for death timer
    public float _deathCount;

    public GameObject Enemy;

    public float _position;
    public float _defenseMeter;
    public float _defenseMax;

    public float _enemyAttackCountdown;

    public string _keyPressed;

    public float _beat = 6/19f;
    public float _cd;

    public bool _breakdown = false;

    //Sprites
    public Sprite _plStatic;
    public Sprite _plAttack;
    public Sprite _plDefend;
    public Sprite _plBlocked;
    public Sprite _plHit;
    public Sprite _plDefeat;
    public Sprite _plDeath;

    // Start is called before the first frame update
    void Start()
    {
        _playerEnter = false;

        _xStart = -0.5f;
        _yStart = -0.695f;

        _gameStart = false;
        _gameOver = false;
        _win = false;
        _lose = false;

        //renderer = GetComponent<Renderer>();
        _position = -0.5f;
        _defenseMeter = 0;
        _defenseMax = 100;
        _enemyAttackCountdown = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameStart && _playerEnter)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, new Vector2(_xStart, _yStart), 0.02f);
        }

        if (_cd < 0 && !_gameOver && _gameStart)
        {
            this.GetComponent<SpriteRenderer>().sprite = _plStatic;
        }
        // Debug.Log(_defenseMeter);

        _cd -= Time.deltaTime;

        Vector3 newPosition = gameObject.transform.position; 
        newPosition.x = _position;
        if (_gameStart)
            gameObject.transform.position = Vector2.Lerp(this.transform.position, newPosition,  0.02f); 

        if(_defenseMeter <= _defenseMax && !_gameOver && _gameStart) { 
            if (_cd <= 0) {
                //Player Presses Attack
                if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
                {
                    this.GetComponent<SpriteRenderer>().sprite = _plAttack;
                    //Debug.Log("Player Attack");
                    if (_enemyAttackCountdown >= 0)
                    {
                        Debug.Log("Enemy Counterattack!");
                        this.GetComponent<SpriteRenderer>().sprite = _plHit;
                        Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyAttack;
                        _position -= 0.3f;
                        _defenseMeter += 50;
                    }
                    else if (Enemy.GetComponent<Enemy>()._enemyBlockingTime >= 0)
                    {   
                        _defenseMeter += 25;
                        _position += 0.05f;
                        Debug.Log("Enemy Blocked!");
                        this.GetComponent<SpriteRenderer>().sprite = _plBlocked;
                        Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyDefend;
                    }
                    else
                    {
                        _defenseMeter -= 5;
                        _position += 0.2f;
                        Enemy.GetComponent<Enemy>()._defenseMeter += 10;
                        Debug.Log("Successful Hit!");
                        this.GetComponent<SpriteRenderer>().sprite = _plAttack;
                        Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyHit;
                    }
                    _cd = _beat;
                    
                }

                //Player Presses Defense
                else if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(1))
                {
                    this.GetComponent<SpriteRenderer>().sprite = _plDefend;
                    if (Enemy.GetComponent<Enemy>()._attackTime >= 0)
                    {
                        Debug.Log("Successful Defense!");
                        this.GetComponent<SpriteRenderer>().sprite = _plDefend;
                        Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyBlocked;
                        Enemy.GetComponent<Enemy>()._attackTime = -1;
                        Enemy.GetComponent<Enemy>()._defenseMeter += 10;
                        _position -= 0.05f;
                        _defenseMeter += 20;
                        
                    }
                    _cd = _beat/2;
                    
                }

                //Player Presses Rest
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    //reduce defense meter
                    _defenseMeter -= 50;
                    if(_defenseMeter < 0)
                    {
                        _defenseMeter = 0;
                    }
                    _cd = _beat / 2;
                }
            }
        }

        //breakdown cooldown
        if(_defenseMeter > _defenseMax)
        {
            _defenseMeter = 0;
            _cd = 1.5f;
            _breakdown = true;
        }

        if(_cd <= 0)
        {
            _breakdown = false;
        }

        if(_defenseMeter <= 0)
        {
            _defenseMeter = 0;
        }

        #region end/win check
        if (_position >= 2 && !_gameOver && _gameStart)
        {
            Debug.Log("Win");
            _win = true;
            this.GetComponent<SpriteRenderer>().sprite = _plDefend;
            Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyDefeat;
            _deathCount = 3;
        }
        if (_position <= -3 && !_gameOver && _gameStart)
        {
            Debug.Log("Lose");
            _lose = true;
            this.GetComponent<SpriteRenderer>().sprite = _plDefeat;
            Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyAttackCue;
            _deathCount = 3;
        }
        if (_gameOver)
        {
            _deathCount -= Time.deltaTime;
            if (_deathCount <= 0)
            {
                if (_win)
                {
                    this.GetComponent<SpriteRenderer>().sprite = _plAttack;
                    Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyDeath;
                }
                else
                {
                    this.GetComponent<SpriteRenderer>().sprite = _plDeath;
                    Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyAttack;
                }
            }
        }
        #endregion

        _gameOver = (_win || _lose);
    }
}
