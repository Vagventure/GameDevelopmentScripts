using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects Instance { get; private set; }
    [SerializeField] private AudioClipRefSO audioClipRefSO;
    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        PlayerControl.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnTrashedSomething += TrashCounter_OnTrashedSomething;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void TrashCounter_OnTrashedSomething(object sender, System.EventArgs e)
    {

        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefSO.trash[Random.Range(0, audioClipRefSO.trash.Length)], trashCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.objectPickup[Random.Range(0, audioClipRefSO.objectPickup.Length)], PlayerControl.Instance.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefSO.objectDrop[Random.Range(0, audioClipRefSO.objectDrop.Length)], baseCounter.transform.position);

    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefSO.chop[Random.Range(0,audioClipRefSO.chop.Length)],cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFail(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = sender as DeliveryCounter;
        PlaySound(audioClipRefSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = sender as DeliveryCounter;
        PlaySound(audioClipRefSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefSO.footsteps, position, volume);
    }
    
}
