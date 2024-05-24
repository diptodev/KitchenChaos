using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTemplate : MonoBehaviour
{
    [SerializeField] private Image imageIcon;
    // Start is called before the first frame update
   public void SetIngredientIcon(KitchenObjectSO kitchenObjectSO)
    {
        imageIcon.sprite = kitchenObjectSO.sprite;
    }
}
