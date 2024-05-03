using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject Enemy;

    public float _position;
    public int _defenseMeter;
    public int _defenseMax;

    public float _enemyAttackCountdown;

    public string _keyPressed;

    public float _beat = 6/19f;
    public float _cd;

    // Start is called before the first frame update
    void Start()
    {
        _position = 0;
        _defenseMeter = 0;
        _enemyAttackCountdown = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_enemyAttackCountdown);
        _cd -= Time.deltaTime;

        Vector3 newPosition = gameObject.transform.position; 
        newPosition.x = _position;
        gameObject.transform.position = newPosition; 


        if (_cd <= 0) {
            //Player Presses Attack
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Debug.Log("Player Attack");
                if (_enemyAttackCountdown >= 0)
                {
                    Debug.Log("Enemy Counterattack!");
                    _position -= 1;
                    _defenseMeter += 30;
                }
                else if (Enemy.GetComponent<Enemy>()._enemyBlockingTime >= 0)
                {   
                    _defenseMeter += 30;
                    _position -= 0.5f;
                    Debug.Log("Enemy Blocked!");
                }
                else
                {
                    _position += 0.3f;
                    Debug.Log("Successful Hit!");
                }
                _cd = _beat/2;
            }

            //Player Presses Defense
            else if (Input.GetKeyDown(KeyCode.J))
            {
                if(Enemy.GetComponent<Enemy>()._attackTime >= 0)
                {
                    Debug.Log("Successful Defense!");
                    Enemy.GetComponent<Enemy>()._attackTime = -1;
                    _position += 1f;
                }

                _cd = _beat/2;

            }
        }
    }
}
