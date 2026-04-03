using UnityEngine;

public class BakeCounter_Visual : MonoBehaviour
{
    [SerializeField] private GameObject counterWithOven;
    [SerializeField] private GameObject microLight;

    private BakeCounter bakeCounter;
    private void Start()
    {
        bakeCounter = counterWithOven.GetComponent<BakeCounter>();

        bakeCounter.OnBaking += BakeCounter_OnBaking;
    }

    private void BakeCounter_OnBaking(object sender, BakeCounter.OnBakingEventsArgs e)
    {
        microLight.SetActive(e.isBaking);
    }
}
