using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeDeliveredCount;
    [SerializeField] private Button restartButton;
    
    private void Start()
    {
        CakeSimGameObject.Instance.OnStateChange += CakeSimGameObject_OnStateChange;

        Hide();
    }

    private void Awake()
    {
        restartButton.onClick.AddListener(() => { RestartGame(); });
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

    private void RestartGame()
    {
        Loader.Load(Loader.Scene.SampleScene);
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
