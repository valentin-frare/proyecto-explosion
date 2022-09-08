using Cinemachine;
using UnityEngine;

public class CameraControl
{   
    private CinemachineFramingTransposer cinemachineFramingTransposer;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    public CameraControl(CinemachineVirtualCamera cinemachineVirtualCamera)
    {
        this.cinemachineVirtualCamera = cinemachineVirtualCamera;
        this.cinemachineFramingTransposer = this.cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void VehicleOnTop()
    {
        if (cinemachineFramingTransposer.m_TrackedObjectOffset.z < 11.66f)
        {
            cinemachineFramingTransposer.m_TrackedObjectOffset.z += Time.fixedDeltaTime;
        }
    }

    public void VehicleOnCenter()
    {
        if (cinemachineFramingTransposer.m_TrackedObjectOffset.z > -5.3f)
        {
            cinemachineFramingTransposer.m_TrackedObjectOffset.z -= Time.fixedDeltaTime;
        }
    }

    public void VehicleOnBotton()
    {
        if (cinemachineFramingTransposer.m_TrackedObjectOffset.z > -10.66f)
        {
            cinemachineFramingTransposer.m_TrackedObjectOffset.z -= Time.fixedDeltaTime;
        }
    }
}
