using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailController : MonoBehaviour
{

    private Animator animator;
    private int trailParamID;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        trailParamID = Animator.StringToHash("Moving");
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = false;
        if (Input.GetKey(KeyCode.W))
        {
            isMoving = true;

        }
        animator.SetBool(trailParamID, isMoving);
    }
}
