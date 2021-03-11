using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{

    public GameObject Cam;
    public float speed;
    public int maxCamPos;

    private Rigidbody2D rigidB;
    private float CurrentSpeed;
    public bool behind;

    // Start is called before the first frame update
    void Start()
    {
        
        CurrentSpeed = speed;
        rigidB = GetComponent<Rigidbody2D>();
        behind = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (behind)
        {
            CurrentSpeed = 0;
        }
        else
        {
            CurrentSpeed = speed;
        }

        if (transform.position.x >= maxCamPos){
            CurrentSpeed = 0;
        }
        
        
        

        rigidB.velocity = new Vector2(CurrentSpeed, 0);
        
        /* transform.position =
             new Vector3(
                 Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
                 Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
                 transform.position.z);*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            behind = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            behind = false;
        }
    }


}
