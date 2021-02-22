using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarginController : MonoBehaviour
{

    private Rigidbody2D rigidb;
    // Start is called before the first frame update
    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 pos = transform.position;


        if (collision.gameObject.tag == "MarginU")
        {
            pos.y = pos.y * (-1) + 10;

        }
        else if (collision.gameObject.tag == "MarginD")
        {
            pos.y = pos.y * (-1) - 10;
        }

        if (collision.gameObject.tag == "MarginR")
        {
            pos.x = pos.x * (-1) + 10;
        }
        else if (collision.gameObject.tag == "MarginL")
        {
            pos.x = pos.x * (-1) - 10;
        }

        transform.position = new Vector3(pos.x, pos.y, pos.z);

    }

}
