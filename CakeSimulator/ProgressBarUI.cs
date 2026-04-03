using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject objectWithProgressBar;
    [SerializeField] private Image bar;

    private CuttingCounter progressBar;
    private void Start()
    {
        progressBar = objectWithProgressBar.GetComponent<CuttingCounter>();
        progressBar.OnProgressChanged += ProgressBar_OnProgressChanged;

        bar.fillAmount = 0;

        Hide();
    }

    private void ProgressBar_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        bar.fillAmount = e.progressNormaliazed;
        if (e.progressNormaliazed == 0 ||  e.progressNormaliazed == 1)
        {
            //Hide the bar
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
