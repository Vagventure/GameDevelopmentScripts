using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OpenContainerTopDoor += ContainerCounter_OpenContainerTopDoor;
    }

    private void ContainerCounter_OpenContainerTopDoor(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
