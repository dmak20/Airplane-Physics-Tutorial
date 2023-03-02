using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class DM_Airplane_Engine : MonoBehaviour
    {
        #region Variables
        public float maxForce = 200f;
        public float maxRPM = 2550f;

        public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Header("Propellers")]
        public DM_Airplane_Propellers propeller;
        #endregion

        #region Builtin Methods
        #endregion

        #region Custom Methods
        public Vector3 CalculateForce(float throttle)
        {
            float finalThrottle = Mathf.Clamp01(throttle);
            finalThrottle = powerCurve.Evaluate(finalThrottle);

            float currentRPM = finalThrottle * maxRPM;
            if (propeller)
            {
                propeller.HandlePropeller(currentRPM);
            }

            float finalPower = finalThrottle * maxForce;
            Vector3 finalForce = transform.forward * finalPower;

            return finalForce;
        }
        #endregion
    }
}