using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineFreeLook freeLookCamera;
    private float originalBias;
    [SerializeField] private float cameraChangeBias = 180;

    private void Awake()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
        originalBias = freeLookCamera.m_Heading.m_Bias;
    }
    public void ChangeCameraRotation()
    {
        if (freeLookCamera.m_Heading.m_Bias == originalBias)
        {
            freeLookCamera.m_Heading.m_Bias = cameraChangeBias;
        }
        else
        {
            freeLookCamera.m_Heading.m_Bias = originalBias;
        }
    }
}
