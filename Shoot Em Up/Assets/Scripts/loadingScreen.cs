using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class loadingScreen : MonoBehaviour
{
    private float transitionDuration = 2500f;
    private float currentDuration;
    private Rigidbody2D rigidB;
    // Start is called before the first frame update
    void Start(){
        rigidB = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float delta = Time.deltaTime * 1000;

        currentDuration += delta;
        if (currentDuration > transitionDuration){

            SceneManager.LoadScene("BossFight");
        }
    }
    // Update is called once per frame
    void FixedUpdate(){
        rigidB.velocity = new Vector2(0, 3000);
    }

}
