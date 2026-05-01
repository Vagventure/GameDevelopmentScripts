using UnityEngine;
using UnityEngine.UI;

public class GamePlayClockUI : MonoBehaviour
{
    [SerializeField] private Image clockImage;

    private float maxGamePlayTime = 30f;
    private float currGamePlayTime = 0f;
    private void Update()
    {
        currGamePlayTime += Time.deltaTime;
        clockImage.fillAmount = currGamePlayTime / maxGamePlayTime;

        if(currGamePlayTime > maxGamePlayTime)
        {
            Time.timeScale = 0f;
        }
    }
}
