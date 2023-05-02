using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DM_Airplane_EngineCutoff : MonoBehaviour
{
    #region Variables
    [Header("Engine Cutoff Properties")]
    public KeyCode cutoffKey = KeyCode.O;
    public KeyCode turnOnKey = KeyCode.P;
    public UnityEvent onEngineCutoff = new UnityEvent();
    public UnityEvent onEngineTurnOn = new UnityEvent();
    #endregion


    #region Built-in Methods
    void Update()
    {
        if (Input.GetKeyDown(cutoffKey))
        {
            HandleEngineCutoff();
        }
        if (Input.GetKeyDown(turnOnKey))
        {
            HandleEngineTurnOn();
        }
    }

    private void HandleEngineTurnOn()
    {
        if (onEngineTurnOn != null)
        {
            onEngineTurnOn.Invoke();
        }
    }

    #endregion

    #region Custom Methods
    private void HandleEngineCutoff()
    {
        if (onEngineCutoff != null)
        {
            onEngineCutoff.Invoke();
        }
    }

    #endregion
}
