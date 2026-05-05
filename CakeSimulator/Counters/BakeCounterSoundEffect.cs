using UnityEngine;

public class BakeCounterSoundEffect : MonoBehaviour
{
    [SerializeField] private BakeCounter bakeCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        bakeCounter.OnMicrowaveStateChanged += BakeCounter_OnMicrowaveStateChanged;
    }

    private void BakeCounter_OnMicrowaveStateChanged(object sender, BakeCounter.OnMicrowaveStateChangedEventArgs e)
    {
        bool playSound = e.state == BakeCounter.State.Baking || e.state == BakeCounter.State.Burning;
        if (playSound)
        {
            Debug.Log(e.state);
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

}
