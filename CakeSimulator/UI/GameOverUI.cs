using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeDeliveredCount;
    
    private void Start()
    {
        CakeSimGameObject.Instance.OnStateChange += CakeSimGameObject_OnStateChange;

        Hide();
    }

    private void CakeSimGameObject_OnStateChange(object sender, System.EventArgs e)
    {
        if (CakeSimGameObject.Instance.IsGameOver())
        {
            Show();

            recipeDeliveredCount.text = DeliveryManager.Instance.GetRecipeDeliveredCount().ToString();
        }
        else
        {
            Hide();
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
