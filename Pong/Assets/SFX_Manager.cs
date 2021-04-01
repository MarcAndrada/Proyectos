using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{

    public AudioClip GoalSound;
    public AudioClip PaddleSound;
    public AudioClip WallSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Goal"){
            audioSource.PlayOneShot(GoalSound);

        }else if(collision.gameObject.tag == "Wall"){
            audioSource.PlayOneShot(WallSound);
        }
        else if (collision.gameObject.tag == "Paddle" ){
            audioSource.PlayOneShot(PaddleSound);
        }

    }

}
