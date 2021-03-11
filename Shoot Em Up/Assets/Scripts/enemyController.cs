using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float speedV;
    public float speedH;

    public GameObject bulletPrefab;
    public float offsetBullet;
    public float fireRate;


    private GameObject bullet;
    private GameObject player;
    private enum DirectionV { NONE, UP, DOWN };
    private enum DirectionH { NONE, LEFT, RIGHT }
    private DirectionV enemyDirectionV = DirectionV.NONE;
    private DirectionH enemyDirectionH = DirectionH.NONE;
    private bool rotation;

    private float currentSpeedV = 0.0f;
    private float currentSpeedH = 0.0f;
    private Rigidbody2D rigidBody;

    private float TimeToShoot;

    private Animator animator;
    private int destroyParamID;
    private bool destroyed = false;
    private float destroyTimer;
    private float animationDuration = 540f;


    // Start is called before the first frame update
    void Start()
    {
        rotation = true;
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        destroyParamID = Animator.StringToHash("Destroyed");


    }

    // Update is called once per frame
    void Update()
    {


        float delta = Time.deltaTime * 1000;

        enemyDirectionV = DirectionV.NONE;
        enemyDirectionH = DirectionH.NONE;

        if (player.transform.position.y <= transform.position.y && player.transform.position.y >= transform.position.y)
        {
            enemyDirectionV = DirectionV.NONE;
        }
        else if (player.transform.position.y < transform.position.y && player.transform.position.y - transform.position.y <= 3 && player.transform.position.y - transform.position.y <= -3)
        {
            enemyDirectionV = DirectionV.DOWN;
        }
        else if (player.transform.position.y > transform.position.y && player.transform.position.y - transform.position.y >= 3 && player.transform.position.y - transform.position.y >= -3)
        {
            enemyDirectionV = DirectionV.UP;
        }


        if (player.transform.position.x < transform.position.x)
        {
            if (!rotation)
            {
                rotation = true;
                transform.rotation = Quaternion.Inverse(gameObject.transform.rotation);
            }
            enemyDirectionH = DirectionH.RIGHT;

        }else if (player.transform.position.x > transform.position.x){
            if (rotation){

                transform.rotation = Quaternion.Inverse(gameObject.transform.rotation);
                rotation = false;

            }

            enemyDirectionH = DirectionH.LEFT;
        }


        TimeToShoot += delta;

        if (TimeToShoot > fireRate)
        {
            Vector3 pos = transform.up * offsetBullet + transform.position;
            bullet = Instantiate(bulletPrefab, pos, transform.rotation);
            Destroy(bullet, 3);
            TimeToShoot = 0;
        }

        if (destroyed)
        {
            destroyTimer += delta;
            if (destroyTimer > animationDuration)
            {
                gameObject.SetActive(false);
            }

        }


    }

    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        currentSpeedV = 0;
        currentSpeedH = 0;

        switch (enemyDirectionV){
            default:
                break;
            case DirectionV.UP:
                currentSpeedV = speedV;

                break;
            case DirectionV.DOWN:
                currentSpeedV = -speedV;
                break;
        }

        switch (enemyDirectionH){
            default:
                break;
            case DirectionH.LEFT:
                currentSpeedH = -speedH;

                break;
            case DirectionH.RIGHT:
                currentSpeedH = speedH;
                
                break;

        }
        rigidBody.velocity = new Vector2(currentSpeedH, currentSpeedV * delta);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullets")
        {
            rigidBody.velocity = new Vector3(0, 0, 0);

            //animacion
            destroyed = true;
            animator.SetTrigger(destroyParamID);
            
        }

    }

}
