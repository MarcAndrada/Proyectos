using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Paddle : MonoBehaviour
{
    public enum Controller { NONE, PLAYER1, PLAYER2, AI };
    public enum Direction { NONE, UP, DOWN };
    public Controller paddleController = Controller.NONE;
    public Direction paddleDirection = Direction.NONE;

    public float baseSpeed = 0.3f;
    public float ballY;
    public float paddleY;

    private float currentSpeedV = 0.0f;
    private Rigidbody2D rigidBody;
   
    GameObject ball;
    GameObject paddle;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    private void Update()
    {
        float delta = Time.deltaTime * 1000;
        KeyCode upButton = KeyCode.None;
        KeyCode downButton = KeyCode.None;
        paddleDirection = Direction.NONE;
        switch (paddleController)
        {
            default: break;
            case Controller.PLAYER1:
                upButton = KeyCode.W;
                downButton = KeyCode.S;
                break;
            case Controller.PLAYER2:
                upButton = KeyCode.UpArrow;
                downButton = KeyCode.DownArrow;
                break;
            case Controller.AI:
                ball = GameObject.FindGameObjectWithTag("Ball");
                paddle = GameObject.Find("Paddle Player 2");
                ballY = ball.transform.position.y;
                paddleY = paddle.transform.position.y;
     
                if(ballY <= paddleY && ballY >= paddleY) {
                    paddleDirection = Direction.NONE;
                }else if (ballY < paddleY && ballY - paddleY <= 0.3 && ballY - paddleY <= -0.3){
                    paddleDirection = Direction.DOWN;
                }else if(ballY > paddleY && ballY - paddleY >= 0.3 && ballY - paddleY >= -0.3){
                    paddleDirection = Direction.UP;
                }
                break;
        }

       
        if(upButton != KeyCode.None && downButton != KeyCode.None){
            if (Input.GetKey(upButton)){
                paddleDirection = Direction.UP;
            } else if (Input.GetKey(downButton)){
                paddleDirection = Direction.DOWN;
            }
        }
    }

    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        currentSpeedV = 0;
        if(paddleDirection == Direction.UP) {
            currentSpeedV = baseSpeed;
        }else if (paddleDirection == Direction.DOWN) {
            currentSpeedV = -baseSpeed;
        }
        rigidBody.velocity = new Vector2(0, currentSpeedV * delta);
    }
}
