using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] private GameObject stovePanelGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
    }

    private void StoveCounter_OnStoveStateChanged(object sender, StoveCounter.OnStoveStateChangedEventAgrs e)
    {
        bool showVisual= e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stovePanelGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
