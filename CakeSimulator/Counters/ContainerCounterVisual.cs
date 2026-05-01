using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_DOOR = "OpenDoor";
    [SerializeField] CounterContainer counterContainer;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
       
    }

    private void Start()
    {
        counterContainer.OnInteractionPerformed += CounterContainer_OnInteractionPerformed;
    }

    private void CounterContainer_OnInteractionPerformed(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_DOOR);
    }
}
