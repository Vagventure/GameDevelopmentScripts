using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private const string Is_Walking = "IsWalking";
    [SerializeField] private PlayerControl player;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(Is_Walking, player.IsWalking());
    }


}
