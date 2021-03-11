using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public float speed;


    private Animator animator;
    private int HitParamID;
    private bool hit = false;
    private float destroyTimer;
    private float animationDuration = 200f;


    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        HitParamID = Animator.StringToHash("HIT");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        float delta = Time.deltaTime * 1000;
        if (hit)
        {

            destroyTimer += delta;
            if (destroyTimer > animationDuration)
            {
                Destroy(gameObject);
            }
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Map" )
        {
            /*Vector3 pos = transform.position * 0 + transform.position;
            Destroy(gameObject);
            bulletHit = Instantiate(bulletPrefab, pos, transform.rotation);*/
            gameObject.tag = "Untagged";
            speed = 0;
            hit = true;
            animator.SetTrigger(HitParamID);
        }

 




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Vector3 pos = transform.position * 0 + transform.position;
        Destroy(gameObject);
        bulletHit = Instantiate(bulletPrefab, pos, transform.rotation);*/
        if (collision.gameObject.tag == "Map"){
            speed = 0;
            hit = true;
            animator.SetTrigger(HitParamID);
        }
        if (collision.gameObject.tag == "Player"){
            speed = 0;
            hit = true;
            animator.SetTrigger(HitParamID);
        }

        if (collision.gameObject.tag == "Enemy"){
            speed = 0;
            hit = true;
            animator.SetTrigger(HitParamID);
        }
    }
}
