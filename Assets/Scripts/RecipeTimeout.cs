using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTimeout : MonoBehaviour
{
    [SerializeField] private Image timeoutImage;
    private float timeoutTime;
    // Update is called once per frame
    void Update()
    {
        timeoutTime = GameManager.Instance.GetRecipeTimeout();
        if (timeoutTime >= 0)
        {
            timeoutImage.fillAmount = timeoutTime;
            if (timeoutTime > 0.65)
            {
                timeoutImage.color = Color.red;
            }
            else
            {
                timeoutImage.color = Color.green;
            }
        }
    }
}
