using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidController : MonoBehaviour
{


    public float speed;
    public GameObject asteroidPrefab;


    private Rigidbody2D rigidb;
    private Animator animator;

    private int HitParamID;
    private GameObject newAsteroid;
    private bool bulletHit;
    private float animationDuration = 450f;
    private float destroyTimer;

    private shipController score;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();

        rigidb.drag = 0;
        rigidb.angularDrag = 0;
        rigidb.velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * speed;
        rigidb.angularVelocity = Random.Range(-50f, 50f);

        animator = GetComponent<Animator>();
        HitParamID = Animator.StringToHash("Hitted");
    }

    // Update is called once per frame
    void Update()
    {

        float delta = Time.deltaTime * 1000;

        if (bulletHit)
        {
            destroyTimer += delta;
            if (destroyTimer > animationDuration)
            {
                bulletHit = false;
                destroyTimer = 0;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullets")
        {
            collision.gameObject.tag = "Untagged";
            if (gameObject.name == "asteroid-Big")
            {
                asteroidsFunction();
                score.asteroidBigScore();   
            }else if (gameObject.name == "asteroid-Medium" || gameObject.name == "asteroid-Medium(Clone)"){
                asteroidsFunction();
                score.asteroidMediumScore();
            }else{
                bulletHit = true;
                rigidb.velocity = new Vector3(0, 0, 0);
                animator.SetTrigger(HitParamID);
                score.asteroidSmallScore();
            }

            //StartCoroutine(AsteroidDestroyed());

        }

    }


    private void asteroidsFunction()
    {

        Vector3 pos = transform.position * 0 + transform.position;
        newAsteroid = Instantiate(asteroidPrefab, pos, transform.rotation);
        newAsteroid = Instantiate(asteroidPrefab, pos, transform.rotation);
        newAsteroid = Instantiate(asteroidPrefab, pos, transform.rotation);
        Destroy(gameObject);
    }
    /* IEnumerator AsteroidDestroyed() NO UTILIZAR
     {
         yield return new WaitForSeconds(0.5f);
        Destroy(bulletColl);

     }*/
}
