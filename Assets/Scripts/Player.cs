using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   private float moveSpeed=5f;
    private float rotateSpeed = 15f;
    private bool isWalking = false;
    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.DownArrow)){
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            inputVector.x -= 1;
        }

        if (Input.GetKey(KeyCode.RightArrow)){

            inputVector.x += 1;
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            inputVector.y += 1;
        }
        
         inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        transform.position += moveDir*Time.deltaTime*moveSpeed;
        transform.forward=Vector3.Lerp(transform.forward,moveDir,Time.deltaTime*rotateSpeed);
    }
    public bool IsWalking => isWalking;
}
