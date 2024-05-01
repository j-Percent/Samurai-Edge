using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject Enemy;

    public int _defenseMeter;
    public int _defenseMax;

    public float _enemyAttackCountdown;
    public float _enemyAttackWaitTime;

    public string _keyPressed;

    // Start is called before the first frame update
    void Start()
    {
        _defenseMeter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Player Attack");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Player Defend");
        }


        if (_enemyAttackCountdown >= 0)
        {
            if (_enemyAttackCountdown <= 0)
            {

            }
            _enemyAttackCountdown -= Time.deltaTime;
        }
    }
}
