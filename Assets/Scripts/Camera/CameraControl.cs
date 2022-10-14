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
        if (cinemachineFramingTransposer.m_TrackedObjectOffset.z < 60.66f)
        {
            cinemachineFramingTransposer.m_TrackedObjectOffset.z += Time.fixedDeltaTime;
        }
    }

    public void VehicleOnCenter()
    {
        if (cinemachineFramingTransposer.m_TrackedObjectOffset.z > 30.3f)
        {
            cinemachineFramingTransposer.m_TrackedObjectOffset.z -= Time.fixedDeltaTime;
        }
    }

    public void VehicleOnBotton()
    {
        if (cinemachineFramingTransposer.m_TrackedObjectOffset.z > 15.66f)
        {
            cinemachineFramingTransposer.m_TrackedObjectOffset.z -= Time.fixedDeltaTime;
        }
    }

    public void ResetView()
    {
        cinemachineFramingTransposer.m_TrackedObjectOffset.z = 30f;
    }
}
