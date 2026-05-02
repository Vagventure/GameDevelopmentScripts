using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    private float volume;

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
