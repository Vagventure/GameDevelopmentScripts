using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] visualGameObjects;
    [SerializeField] private BaseCounter baseCounter;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
      if(e.selectedCounter == baseCounter)
      {
         Show();
      }
      else
      {
         Hide();   
      }
    }


    private void Show()
    {
        foreach (GameObject gameObject in visualGameObjects) { 
        gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject gameObject in visualGameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}
