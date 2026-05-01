using System;
using TMPro;
using UnityEngine;

public class GameStartCountDownTimerUI : MonoBehaviour
{
    private TextMeshProUGUI countDownTimer;

    private void Awake()
    {
        countDownTimer = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Start()
    {
        CakeSimGameObject.Instance.OnStateChange += CakeSimGameObject_OnStateChange;
        Hide();
    }

    private void CakeSimGameObject_OnStateChange(object sender, System.EventArgs e)
    {
        if (CakeSimGameObject.Instance.IsCountDownTimerOn()) 
        {
            Show();

        }
        else
        {
            Hide();
        }

    }

    private void Update()
    {
        countDownTimer.text = Mathf.Ceil(CakeSimGameObject.Instance.GetCountDownTimer()).ToString();
    }
   
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
   
}
