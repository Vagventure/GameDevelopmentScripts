using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private string CUT = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_onCut;
    }

    private void CuttingCounter_onCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }

}
