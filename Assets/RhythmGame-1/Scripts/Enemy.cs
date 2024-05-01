using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject Player;

    public int _distance;
    public int _defenseMeter;
    public int _defenseMax;

    public float _attackCountdown;
    public float _attackWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        _distance = 0;
        _defenseMeter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackCountdown >= 0)
        {
            _attackCountdown -= Time.deltaTime;
            if (_attackCountdown <= 0)
            {

            }
        }
    }

    public void _attack()
    {
        Debug.Log("Attack");
    }
    public void _block()
    {
        Debug.Log("Block");
    }
    public void _wait()
    {
        Debug.Log("Wait");
    }
}
