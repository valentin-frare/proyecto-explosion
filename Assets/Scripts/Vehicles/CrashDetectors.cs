using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CrashBoxes
{
    public Transform detectorBox;
    public GameObject crashedModel;
    public bool isCrashed;
}

[Serializable]
public class CrashDetectors
{
    [SerializeField] private GameObject defaultModel;
    [SerializeField] public CrashBoxes[] crashBoxes;
    [SerializeField] private LayerMask layerMask;

    public Action OnVehicleCrashed;

    public void Update()
    {
        foreach (var crashBox in crashBoxes)
        {
            if (Physics.OverlapBox(crashBox.detectorBox.position, crashBox.detectorBox.localScale / 2, Quaternion.identity, layerMask).Length > 0)
            {
                crashBox.crashedModel.SetActive(true);
                defaultModel.SetActive(false);
                crashBox.isCrashed = true;
                OnVehicleCrashed.Invoke();
                return;
            }
        }

        defaultModel.SetActive(true);

        foreach (var crashBox in crashBoxes)
        {
            crashBox.crashedModel.SetActive(false);
            crashBox.isCrashed = false;
        }
    }
}