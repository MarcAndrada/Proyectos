using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemys : MonoBehaviour
{

    private enemyController Controller;
    // Start is called before the first frame update
    void Start()
    {
       
        Controller = GetComponent<enemyController>();
        Controller.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TriggerActivate")
        {
            Controller.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TriggerActivate")
        {
            Controller.enabled = false;
        }
    }
}
