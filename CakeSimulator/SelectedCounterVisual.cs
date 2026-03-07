using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] visualGameObjects;
    [SerializeField] private ClearCounter clearCounter;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
      if(e.selectedCounter == clearCounter)
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
