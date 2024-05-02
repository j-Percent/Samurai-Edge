using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject Enemy;

    public int _defenseMeter;
    public int _defenseMax;

    public float _enemyAttackCountdown;

    public string _keyPressed;

    public float _beat = 6/19f;
    public float _cd;

    // Start is called before the first frame update
    void Start()
    {
        _defenseMeter = 0;
        _enemyAttackCountdown = -1;
    }

    // Update is called once per frame
    void Update()
    {

        _cd -= Time.deltaTime;

        if(_cd <= 0) {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Debug.Log("Player Attack");

                if (_enemyAttackCountdown >= 0)
                {
                    //Debug.Log("Enemy Counterattack!"); 
                }
                else if (Enemy.GetComponent<Enemy>()._blocking >= 0)
                {
                    _defenseMeter += 30;
                   // Debug.Log("Enemy Blocked!");
                }
                else
                {
                    //Debug.Log("Successful Hit!");
                }
                _cd = _beat;
            }

            else if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("Player Defend");
                if(_enemyAttackCountdown >= 0)
                {
                    Debug.Log("Successful Defense!");
                    _enemyAttackCountdown = -1;
                }
                _cd = _beat;

            }
        }

        if (_enemyAttackCountdown >= 0)
        {
            _enemyAttackCountdown -= Time.deltaTime;
            if (_enemyAttackCountdown <= 0)
            {
               // Debug.Log("Player Hit!");
            }
            _cd = _beat;
        }



    }
}
