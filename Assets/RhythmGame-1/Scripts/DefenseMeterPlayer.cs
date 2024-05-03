using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseMeterPlayer : MonoBehaviour
{

    public GameObject Player;
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
        if(Player.GetComponent<Player>()._breakdown == true)
        {
            renderer.material.color = new Color(255, 0, 0);
            GetComponent<Transform>().localScale = new Vector3(3, 0.3f, 0.3f);
        }
        else if (Player.GetComponent<Player>()._cd > 0)
        {
            renderer.material.color = new Color(255, 0, 0);
            GetComponent<Transform>().localScale = new Vector3(Player.GetComponent<Player>()._defenseMeter * 0.03f, 0.3f, 0.3f);
            
        }
        else
        {
            //Bar Longer/Shorter
            renderer.material.color = new Color(255, 255, 255);
            GetComponent<Transform>().localScale = new Vector3(Player.GetComponent<Player>()._defenseMeter * 0.03f, 0.3f, 0.3f);
        }

    }
}
