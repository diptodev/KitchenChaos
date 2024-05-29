using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisuals : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform platePrefabs;
    [SerializeField] private Transform counterTopPoint;
    List<GameObject> platePrefabsList;
    // Start is called before the first frame update
    void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnSpawnedRemoved += PlateCounter_OnSpawnedRemoved;
        platePrefabsList = new List<GameObject>();
    }

    private void PlateCounter_OnSpawnedRemoved(object sender, System.EventArgs e)
    {
        GameObject lastEnter = platePrefabsList[platePrefabsList.Count - 1];
        platePrefabsList.Remove(lastEnter);
        Destroy(lastEnter);
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform platePrefabsVisual = Instantiate(platePrefabs, counterTopPoint);
        float plateOffsetY = 0.1f;
        platePrefabsVisual.localPosition = new Vector3(0, plateOffsetY * platePrefabsList.Count, 0);
        platePrefabsList.Add(platePrefabsVisual.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
