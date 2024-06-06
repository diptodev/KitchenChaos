using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStaticField : MonoBehaviour
{
    private void Awake()
    {
        GameInput.ClearStaticData();
        DeliveryCounter.ClearStaticData();
        TrashCounter.ClearStaticData();
        CuttingCounter.ClearStaticData();

        CompletePlateVisual.ClearStaticData();
        Player.ClearStaticData();
        Time.timeScale = 1f;

    }
}
