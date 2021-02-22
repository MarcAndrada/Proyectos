using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;

    
    private Animator animator;
    private GameObject bulletHit;
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
        if (hit){

            destroyTimer += delta;
            if (destroyTimer > animationDuration){
                hit = false;
                destroyTimer = 0;
                Destroy(gameObject);
            }
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid"){
            speed = 0;
            /*Vector3 pos = transform.position * 0 + transform.position;
            Destroy(gameObject);
            bulletHit = Instantiate(bulletPrefab, pos, transform.rotation);*/
            
            hit = true;
            speed = 0;
            animator.SetTrigger(HitParamID);
            



        }
    }
}
