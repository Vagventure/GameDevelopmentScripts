using UnityEngine;

public class LoadSreenCallBack : MonoBehaviour
{
    private bool isFirstFrameRender = true;

    private void Update()
    {
        if (isFirstFrameRender)
        {
            isFirstFrameRender = false;

            Loader.LoadCallBack();
        }
    }
}
