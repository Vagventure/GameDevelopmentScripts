using UnityEngine;

public class GamePauseUI : MonoBehaviour
{
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
