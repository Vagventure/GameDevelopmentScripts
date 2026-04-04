using UnityEngine;

public class BakeCounter_Visual : MonoBehaviour
{
    [SerializeField] private GameObject counterWithOven;
    [SerializeField] private GameObject microLight;

    private BakeCounter bakeCounter;
    private void Start()
    {
        bakeCounter = counterWithOven.GetComponent<BakeCounter>();
        bakeCounter.OnMicrowaveStateChanged += BakeCounter_OnMicrowaveStateChanged;
      
    }

    private void BakeCounter_OnMicrowaveStateChanged(object sender, BakeCounter.OnMicrowaveStateChangedEventArgs e)
    {
        bool isBaking = e.state == BakeCounter.State.Baking || e.state == BakeCounter.State.Burning;
        gameObject.SetActive(isBaking);
    }
}
