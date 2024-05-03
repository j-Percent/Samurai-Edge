using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private Renderer renderer;

    public GameObject Enemy;

    public float _position;
    public float _defenseMeter;
    public float _defenseMax;

    public float _enemyAttackCountdown;

    public string _keyPressed;

    public float _beat = 6/19f;
    public float _cd;

    public bool _breakdown = false;

    // Start is called before the first frame update
    void Start()
    {
        //renderer = GetComponent<Renderer>();
        _position = 0;
        _defenseMeter = 0;
        _defenseMax = 100;
        _enemyAttackCountdown = -1;
    }

    // Update is called once per frame
    void Update()
    {

       // Debug.Log(_defenseMeter);

        _cd -= Time.deltaTime;

        Vector3 newPosition = gameObject.transform.position; 
        newPosition.x = _position;
        gameObject.transform.position = newPosition; 

        if(_defenseMeter <= _defenseMax) { 
            if (_cd <= 0) {
                //Player Presses Attack
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //Debug.Log("Player Attack");
                    if (_enemyAttackCountdown >= 0)
                    {
                        Debug.Log("Enemy Counterattack!");
                        _position -= 0.3f;
                        _defenseMeter += 50;
                    }
                    else if (Enemy.GetComponent<Enemy>()._enemyBlockingTime >= 0)
                    {   
                        _defenseMeter += 25;
                        _position -= 0.5f;
                        Debug.Log("Enemy Blocked!");
                    }
                    else
                    {
                        _defenseMeter -= 5;
                        _position += 0.1f;
                        Enemy.GetComponent<Enemy>()._defenseMeter += 10;
                        Debug.Log("Successful Hit!");
                    }
                    _cd = _beat;
                    
                }

                //Player Presses Defense
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    if(Enemy.GetComponent<Enemy>()._attackTime >= 0)
                    {
                        Debug.Log("Successful Defense!");
                        Enemy.GetComponent<Enemy>()._attackTime = -1;
                        Enemy.GetComponent<Enemy>()._defenseMeter += 10;
                        _position -= 0.1f;
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
        if (_position >= 2.75)
        {
            Debug.Log("Win");
        }
        if (_position <= -2.75)
        {
            Debug.Log("Lose");
        }
        #endregion

    }
}
