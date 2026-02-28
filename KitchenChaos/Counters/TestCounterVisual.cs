using UnityEngine;

public class TestCounterVisual : MonoBehaviour
{
    private string OPEN = "Open";

    [SerializeField] private TestCounter testCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        testCounter.OpenTestCounterDoor += TestCounter_OpenTestCounterDoor;
    }

    private void TestCounter_OpenTestCounterDoor(object sender, System.EventArgs e)
    {
        if (animator.GetBool(OPEN)) { 
        animator.SetBool(OPEN, false);
        }
        else
        {
            animator.SetBool(OPEN, true);
        }
    }
}
