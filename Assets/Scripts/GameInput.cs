using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
   public Vector2 GetNormalizedInput()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.x -= 1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {

            inputVector.x += 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputVector.y += 1;
        }

        return inputVector.normalized;
    }
}
