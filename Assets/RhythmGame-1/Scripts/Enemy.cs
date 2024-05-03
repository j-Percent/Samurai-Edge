using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject Player;

    private Renderer renderer;

    public int _distance;
    public int _defenseMeter;
    public int _defenseMax;

    public float _enemyAttackWaitTime;
    

    public float _blocking;

    float _cd = 6 / 19f;
    public float _counter = 0;

    public float _a ;
    public bool _b = false;

    public AudioSource _audio;

    public AudioClip _attackSFX;
    public AudioClip _defendSFX;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();

        _distance = 0;
        _defenseMeter = 0;
        _enemyAttackWaitTime = 12/19;
        _blocking = -1f;

        _audio = GetComponent<AudioSource>();
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

        if(_a >= 0)
        {
            _a -= Time.deltaTime;
        }

        if(_counter <= 0)
        {
            
            if (_b == true)
            {
                _blocking = _cd;
                _b = false;
               
            }
        }

    }

    public void _attack()
    {

       
        _a = _cd;

        renderer.material.color = new Color(255, 0, 0);
        _audio.clip = _attackSFX;
        _audio.Play();
    }
    public void _block()
    {
        /
        _counter = _cd;
        _b = true;
        renderer.material.color = new Color(0, 0, 255);
        _audio.clip = _defendSFX;
        _audio.Play();
    }

    public void _wait()
    {
        
        renderer.material.color = new Color(0, 255, 0);
    }
}
