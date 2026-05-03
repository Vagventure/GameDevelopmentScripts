using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            CakeSimGameObject.Instance.TogglePause();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        });
        optionButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsMenuUI.Instance.Show(Show);
        });

    }
    private void Start()
    {
        CakeSimGameObject.Instance.OnGamePause += CakeSimGameObject_OnGamePause;
        CakeSimGameObject.Instance.OnGameResume += CakeSimGameObject_OnGameResume;
        Hide();
    }

    private void CakeSimGameObject_OnGameResume(object sender, System.EventArgs e)
    {
        Hide();

    }

    private void CakeSimGameObject_OnGamePause(object sender, System.EventArgs e)
    {
        Show();

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
