using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    [SerializeField] private AudioSource musicSource;
    private float volume;

    private void Awake()
    {
        Instance = this;
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }

        musicSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }
}
