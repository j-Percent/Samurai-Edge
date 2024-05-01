using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int _distance;
    public int _defenseMeter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
