using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class DM_Airplane_Tachometer : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Tachometer Properties")]
        public RectTransform pointer;
        public DM_Airplane_Engine engine;
        public float maxRPM = 3500f;
        public float maxRotation = 312;
        public float pointerSpeed = 2f;

        private float finalRotation = 0f;

        #endregion


        #region BuiltIn Methods
        #endregion

        #region Interface Methods
        public void HandleAirplaneUI()
        {
            if (engine)
            {
                if (pointer)
                {
                    float normalizedRPM = Mathf.InverseLerp(0, maxRPM, engine.CurrentRPMS);
                    float wantedRotation = maxRotation * -normalizedRPM;
                    finalRotation = Mathf.Lerp(finalRotation, wantedRotation, Time.deltaTime * pointerSpeed);
                    pointer.rotation = Quaternion.Euler(0,0,finalRotation);
                }
            }


        }
        #endregion
    } 
}
