using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        LookForward,
        LookForwardInverted
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;

            case Mode.LookAtInverted:
                Vector3 newLookDir = transform.position - Camera.main.transform.forward;
                transform.LookAt(newLookDir);
                break;

            case Mode.LookForward:
                transform.forward = Camera.main.transform.forward;
                break;

            case Mode.LookForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;

        }
    }
}
