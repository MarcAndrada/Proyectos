using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public enum DirectionV { NONE, UP, DOWN };
    public enum DirectionH { NONE, LEFT, RIGHT }
    public DirectionV shipDirectionV = DirectionV.NONE;
    public DirectionH shipDirectionH = DirectionH.NONE;

    public float speedV;
    public float speedH;

    public GameObject bulletPrefab;
    public float offsetBullet;
    public float fireRate;
    public int lives = 3;
    public GameObject Cam;

    public GameObject LivesLeft;

    private float currentSpeedV = 0.0f;
    private float currentSpeedH = 0.0f;
    private Rigidbody2D rigidBody;

    private KeyCode upButton = KeyCode.W;
    private KeyCode downButton = KeyCode.S;
    private KeyCode leftButton = KeyCode.A;
    private KeyCode rightButton = KeyCode.D;

    private bool Shoot;
    private float TimeToShoot;
    private GameObject bullet;

    private Animator animator;
    private int destroyParamID;
    private bool destroyed = false;
    private float destroyTimer;
    private float animationDuration = 430f;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        destroyParamID = Animator.StringToHash("Destroyed");
        LivesLeft.GetComponent<Text>().text = lives.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;
       
        shipDirectionV = DirectionV.NONE;
        shipDirectionH = DirectionH.NONE;

        if (Input.GetKey(upButton))
        {
            shipDirectionV = DirectionV.UP;
        }
        else if (Input.GetKey(downButton))
        {
            shipDirectionV = DirectionV.DOWN;
        }
        
        if (Input.GetKey(leftButton))
        {
            shipDirectionH = DirectionH.LEFT;
        }
        else if (Input.GetKey(rightButton))
        {
            shipDirectionH = DirectionH.RIGHT;
        }

        Shoot = Input.GetKey(KeyCode.Space);

        TimeToShoot += delta;

        if (Shoot){


            if (TimeToShoot > fireRate)
            {
                ShootFunction();
                TimeToShoot = 0;
            }

        }

        if (destroyed)
        {
            destroyTimer += delta;
            if (destroyTimer > animationDuration)
            {

                destroyed = false;
                destroyTimer = 0;

                if (lives > 1)
                {
                    Vector2 starterPos;
                    starterPos = new Vector2(Cam.transform.position.x, Cam.transform.position.y);
                    transform.position = starterPos;
                    
                    lives--;
                    LivesLeft.GetComponent<Text>().text = lives.ToString();
                }
                else{
                    SceneManager.LoadScene("LoseTitle");
                }
            }
        }

    }
    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        currentSpeedV = 0;
        currentSpeedH = 0;

        switch (shipDirectionV)
        {
            default:
                break;
            case DirectionV.UP:
                currentSpeedV = speedV;

                break;
            case DirectionV.DOWN:
                currentSpeedV = -speedV;
                break;
        }

        switch (shipDirectionH)
        {
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
        if (collision.gameObject.tag == "EnemyBullets")
        {
            rigidBody.velocity = new Vector3(0, 0, 0);

            //animacion
            destroyed = true;
            animator.SetTrigger(destroyParamID);
            //Esperar y destruir nave y crear otra

            /*Crear Nueva nave
             * Vector2 starterPos; 
             * starterPos = new Vector2 (Cam.transform.position.x, Cam.transform.position.x);
             * ship = Instantiate(shipPrefab, starterPos, transform.rotation);
            */
        }

        if (collision.gameObject.tag == "Teleporter"){
            SceneManager.LoadScene("LoadingScreen");
        }
       
    }

    public void ShootFunction(){
        
        //StartCoroutine(ShootCD()); 
        Vector3 pos = transform.up * offsetBullet + transform.position;
        bullet = Instantiate(bulletPrefab, pos, transform.rotation);
        Destroy(bullet, 3);

    }


}
