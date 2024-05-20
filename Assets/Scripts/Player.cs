using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   private float moveSpeed=5f;
    [SerializeField] Animator animator;
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
        if(inputVector != Vector2.zero)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
         inputVector = inputVector.normalized;
        Vector3 newPos = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += newPos*Time.deltaTime*moveSpeed;
    }
}
