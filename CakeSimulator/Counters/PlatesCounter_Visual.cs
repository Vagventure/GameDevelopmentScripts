using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlatesCounter_Visual : MonoBehaviour
{
    [SerializeField] Transform counterTopPosition;
    [SerializeField] Transform spawnKitchenObject;
    [SerializeField] PlatesCounter platesCounter;
     
    private List<GameObject> plateVisualGameObjectList;
    void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObj = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObj);
        Destroy(plateGameObj);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e)
    {
        Transform spawnPlateTransform = Instantiate(spawnKitchenObject,counterTopPosition);

        float spawnOffsetY = 0.1f;
        spawnPlateTransform.localPosition = new Vector3(0,spawnOffsetY * plateVisualGameObjectList.Count,0);
        plateVisualGameObjectList.Add(spawnPlateTransform.gameObject);
    }

}
