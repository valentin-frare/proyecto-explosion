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
        if (cinemachineFramingTransposer.m_TrackedObjectOffset.z < 30.66f)
        {
            cinemachineFramingTransposer.m_TrackedObjectOffset.z += Time.fixedDeltaTime;
        }
    }

    public void VehicleOnCenter()
    {
        if (cinemachineFramingTransposer.m_TrackedObjectOffset.z > 15.3f)
        {
            cinemachineFramingTransposer.m_TrackedObjectOffset.z -= Time.fixedDeltaTime;
        }
    }

    public void VehicleOnBotton()
    {
        if (cinemachineFramingTransposer.m_TrackedObjectOffset.z > 6.66f)
        {
            cinemachineFramingTransposer.m_TrackedObjectOffset.z -= Time.fixedDeltaTime;
        }
    }
}
