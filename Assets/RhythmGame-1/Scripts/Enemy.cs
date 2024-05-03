using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject Player;

    public int _distance;
    public int _defenseMeter;
    public int _defenseMax;

    public float _enemyAttackWaitTime;
    

    public float _blocking;

    float _cd = 6 / 19f;
    public float _counter = 0;

    public bool _a = false;
    public bool _b = false;

    // Start is called before the first frame update
    void Start()
    {
        _distance = 0;
        _defenseMeter = 0;
        _enemyAttackWaitTime = 6/19;
        _blocking = -1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (_blocking >= -1)
        {
            _blocking -= Time.deltaTime;
        }

        if (_counter >= -1)
        {
            _counter -= Time.deltaTime;
        }

        if(_counter <= 0)
        {
            if(_a == true)
            {
                Player.GetComponent<Player>()._enemyAttackCountdown = _enemyAttackWaitTime;
                _a = false;
                Debug.Log("Enemy Attack");
            }
            if (_b == true)
            {
                Player.GetComponent<Player>()._enemyAttackCountdown = _enemyAttackWaitTime;
                _blocking = _cd;
                _b = false;
                Debug.Log("Enemy Block");
            }
        }

    }

    public void _attack()
    {
        _counter = _cd;
        _a = true;
    }
    public void _block()
    {
        _counter = _cd;
        _b = true;
    }

    //public void _blockEnd()
    //{
    //    //Debug.Log("Enemy Block End");
    //    _blocking = false;
    //}

    public void _wait()
    {
        Debug.Log("Enemy Wait");
    }
}
