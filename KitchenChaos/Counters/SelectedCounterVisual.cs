using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] visualGameObject;
    [SerializeField] private BaseCounter baseCounter;
    private void Start()
    {
        PlayerControl.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, PlayerControl.OnSelectedCounterChangedArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            UnHide();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        foreach (GameObject visualObj in visualGameObject) { 
        visualObj.SetActive(false);
        }
    }

    private void UnHide()
    {
        foreach (GameObject visualObj in visualGameObject)
        {
            visualObj.SetActive(true);
        }
    }
}
