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
        if(_blocking >= -1)
        _blocking -= Time.deltaTime;
    }

    public void _attack()
    {
        //Debug.Log("Enemy Attack");
        Player.GetComponent<Player>()._enemyAttackCountdown = _enemyAttackWaitTime;
    }
    public void _block()
    {
        //Debug.Log("Enemy Block");
        _blocking = _cd;
    }

    //public void _blockEnd()
    //{
    //    //Debug.Log("Enemy Block End");
    //    _blocking = false;
    //}

    public void _wait()
    {
        //Debug.Log("Enemy Wait");
    }
}
