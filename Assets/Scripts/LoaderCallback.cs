using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    // Start is called before the first frame update
    private bool update = true;

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            update = false;
            SceneLoader.LoaderCallback();
        }
    }
}
