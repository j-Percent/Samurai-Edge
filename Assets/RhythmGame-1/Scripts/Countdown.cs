using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{

    public Sprite _3;
    public Sprite _2;
    public Sprite _1;
    public Sprite _0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _set3()
    {
        this.GetComponent<SpriteRenderer>().sprite = _3;
    }
    public void _set2()
    {
        this.GetComponent<SpriteRenderer>().sprite = _2;
    }
    public void _set1()
    {
        this.GetComponent<SpriteRenderer>().sprite = _1;
    }
    public void _set0()
    {
        this.GetComponent<SpriteRenderer>().sprite = _0;
    }
    public void _destroy()
    {
        Destroy(gameObject);
    }
}
