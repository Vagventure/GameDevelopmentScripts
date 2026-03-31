using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private PlayerControl player;
    private float footstepTimer;
    private float footstepTimerMax;

    private void Awake()
    {
        player = GetComponent<PlayerControl>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if (player.IsWalking())
            {
                float volume = 0.2f;
                SoundEffects.Instance.PlayFootstepsSound(player.transform.position, volume);
                
            }
        }
    }
}
