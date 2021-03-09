using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class shipController : MonoBehaviour
{
    public float accel;
    public float decel;
    public float maxSpeed;
    public float spinSpeed;

    public GameObject bulletPrefab;
    public float offsetBullet;
    public GameObject bullet;
    public float fireRate;

    public GameObject trail;
    public GameObject ship;
    public GameObject shipPrefab;

    public GameObject Score;

    private Animator animator;
    private int destroyParamID;
    private bool destroyed = false;
    private float destroyTimer;
    private float animationDuration = 540f;

    private Rigidbody2D rigidb;
    private float moveY;
    private float SpinZ;

    private bool Shoot;
    private bool cooldown;
    private float TimeToShoot;

    
    private int scoreCounter;


    // Start is called before the first frame update
    void Start()
    {
        rigidb = GetComponent<Rigidbody2D> ();
        rigidb.drag = decel;

        animator = GetComponent<Animator>();
        destroyParamID = Animator.StringToHash("Destroyed");

        cooldown = true;

        scoreCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (scoreCounter >= 3000)
        {
            SceneManager.LoadScene("WinTitle");
        }
        float delta = Time.deltaTime * 1000;
        moveY = Input.GetAxis("Vertical"); ;
        SpinZ = Input.GetAxis("Horizontal");
        Shoot = Input.GetKey(KeyCode.Space);

        TimeToShoot += delta;

        if (Shoot){
            
            
            if (TimeToShoot > fireRate)
            {
                cooldown = true;
                TimeToShoot = 0;
            }

        }

        ShootFunction();

        if (destroyed)
        {
            destroyTimer += delta;
            if (destroyTimer > animationDuration)
            {
            
                Destroy(gameObject);
                SceneManager.LoadScene("LoseTitle");
                destroyed = false;
                destroyTimer = 0;
     
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            asteroidSmallScore();
        }
    }

    private void FixedUpdate()
    {
       
        rigidb.AddForce(transform.up * accel * Mathf.Clamp(moveY, -0.2f, 1f));
        if (rigidb.velocity.magnitude > maxSpeed){
            rigidb.velocity = new Vector2(0f , 0f);
            rigidb.velocity = rigidb.velocity * maxSpeed; 
        }

        if (SpinZ == 0){
            return;
        }

        transform.Rotate(0, 0, -spinSpeed * SpinZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid"){
            rigidb.velocity = new Vector3(0, 0, 0);
            
            //animacion
            destroyed = true;
            Destroy(trail);
            animator.SetTrigger(destroyParamID);
            //Esperar y destruir nave y crear otra
            
            /*Crear Nueva nave
             * Vector3 starterPos; 
             * starterPos = new Vector3 (0f, 0f, 0f);
             * ship = Instantiate(shipPrefab, starterPos, transform.rotation);
            */
            //Esperar y cambiar escena
            // StartCoroutine(ShipDestroyed());

        }

    }


    public void ShootFunction()
    {
        if (Shoot && cooldown){
            //StartCoroutine(ShootCD());   
            Vector3 pos = transform.up * offsetBullet + transform.position;

            bullet = Instantiate(bulletPrefab, pos, transform.rotation);
            Destroy(bullet, 3);
            cooldown = false;
        }

       
    }

    public void asteroidBigScore()
    {
        scoreCounter += 50;
        
        Score.GetComponent<Text>().text = scoreCounter.ToString();

    }

    public void asteroidMediumScore()
    {
        scoreCounter += 100;
        Score.GetComponent<Text>().text = scoreCounter.ToString();
    }

    public void asteroidSmallScore()
    {
        scoreCounter += 150;
        Score.GetComponent<Text>().text = scoreCounter.ToString();
    }

    /* Corrutinas MAL
     IEnumerator ShootCD()
     {
         cooldown = false;
        Vector3 pos = transform.up * offsetBullet + transform.position;

         bullet = Instantiate(bulletPrefab, pos, transform.rotation);
         Destroy(bullet, 7);
         yield return new WaitForSeconds (0.5f);
         cooldown = true;
     }

     IEnumerator ShipDestroyed()
     {
         yield return new WaitForSeconds(0.5f);
         Destroy(ship);
          SceneManager.LoadScene("Lose Title");
     }
    */
}
