using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_SOUND_VOL = "MusicSoundVal";
    public static MusicManager Instance { get; private set; }
    [SerializeField] private AudioSource musicSource;
    private float volume = 0.2f;

    private void Awake()
    {
        Instance = this;

        if (PlayerPrefs.HasKey(PLAYER_PREFS_MUSIC_SOUND_VOL))
        {
            volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_SOUND_VOL, 1f);
        }
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }

        musicSource.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_SOUND_VOL, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
