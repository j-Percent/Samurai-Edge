using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMeterEnemy : MonoBehaviour
{
    public GameObject Enemy;
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        
        


        //Change color when overload
        if (Enemy.GetComponent<Enemy>()._cd > 0)
        {
            renderer.material.color = new Color(255, 0, 0);
            GetComponent<Transform>().localScale = new Vector3(3f, 0.3f, 0.3f);
        }
        else
        {
            //Bar Longer/Shorter
            renderer.material.color = new Color(255, 255, 255);
            GetComponent<Transform>().localScale = new Vector3(Enemy.GetComponent<Enemy>()._defenseMeter * 0.03f, 0.3f, 0.3f);
        }

    }
}
