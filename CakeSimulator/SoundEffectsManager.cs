using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOL = "SoundEffectsVol";
    public static SoundEffectsManager Instance { get; private set; }
    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private float volume = .5f;

    private void Awake()
    {
        Instance = this;

        if (PlayerPrefs.HasKey(PLAYER_PREFS_SOUND_EFFECTS_VOL))
        {
            volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOL, 1f);        
        }
    }
    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;  
        DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;
        TrashCounterScript.OnTrashObject += TrashCounter_OnTrashedSomething;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefSO.objectDrop[Random.Range(0, audioClipRefSO.objectDrop.Length)], baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.objectPickup[Random.Range(0, audioClipRefSO.objectPickup.Length)], Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefSO.chop[Random.Range(0, audioClipRefSO.chop.Length)], cuttingCounter.transform.position);
    }

    private void TrashCounter_OnTrashedSomething(object sender, System.EventArgs e)
    {
        TrashCounterScript trashCounter = sender as TrashCounterScript;
        PlaySound(audioClipRefSO.trash[Random.Range(0, audioClipRefSO.trash.Length)], trashCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFail(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryCounter = sender as DeliveryManager;
        PlaySound(audioClipRefSO.deliveryFail[Random.Range(0, audioClipRefSO.deliveryFail.Length)], deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryCounter = sender as DeliveryManager;
        PlaySound(audioClipRefSO.deliverySuccess[Random.Range(0, audioClipRefSO.deliverySuccess.Length)], deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOL, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }


}
