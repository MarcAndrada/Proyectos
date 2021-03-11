using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossController : MonoBehaviour
{
    public float speedH;
    public enum DirectionH { NONE, LEFT, RIGHT }
    public DirectionH enemyDirectionH = DirectionH.NONE;

    public GameObject bulletPrefab;
    public float offsetBullet;

    public float fireRate;
    public float changeDirT;

    public float HP;



    private int dir;
    private float DirT;

    private float currentSpeedH = 0.0f;
    private Rigidbody2D rigidBody;

    private float TimeToShoot;

    private GameObject bullet;
    private Animator animator;
    private int destroyParamID;
    private bool destroyed = false;
    private float destroyTimer;
    private float animationDuration = 540f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        destroyParamID = Animator.StringToHash("Destroyed");
    }

    // Update is called once per frame
    void Update()
    {

        if (HP <= 0){
            destroyed = true;
            animator.SetTrigger(destroyParamID);
        }
        float delta = Time.deltaTime * 1000;

        enemyDirectionH = DirectionH.NONE;

        DirT += delta;

        if (DirT > changeDirT){
            dir = Random.Range(0, 3);
            DirT = 0;
        }


        if (dir == 0){
            enemyDirectionH = DirectionH.NONE;

        }else if (dir == 1){
            enemyDirectionH = DirectionH.RIGHT;

        }else if (dir == 2){
            enemyDirectionH = DirectionH.LEFT;

        }


        TimeToShoot += delta;

        if (TimeToShoot > fireRate) {
            Vector3 pos = transform.up * offsetBullet + transform.position;
            bullet = Instantiate(bulletPrefab, pos, transform.rotation);
            Destroy(bullet, 3);
            TimeToShoot = 0;
        }

        if (destroyed){

            destroyTimer += delta;
            if (destroyTimer > animationDuration)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene("WinTitle");
            }

        }

    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        currentSpeedH = 0;


        switch (enemyDirectionH)
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

        rigidBody.velocity = new Vector2(currentSpeedH, 0 * delta);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullets"){
            HP--;
        }
    }
}

