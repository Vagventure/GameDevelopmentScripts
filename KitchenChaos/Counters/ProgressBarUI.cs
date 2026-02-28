using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    private IProgressBar progressBar;
    private void Start()
    {
        progressBar = hasProgressGameObject.GetComponent<IProgressBar>();

       progressBar.OnProgressChanged += CuttingCounter_onProgressChanged;
        barImage.fillAmount = 0f;

        Hide();
    }

    private void CuttingCounter_onProgressChanged(object sender, IProgressBar.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormaliazed;

        if(barImage.fillAmount == 0f || barImage.fillAmount == 1f)
        {
            Hide();
        }
        else
        {
            UnHide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UnHide()
    {
        gameObject.SetActive(true);
    }
}
