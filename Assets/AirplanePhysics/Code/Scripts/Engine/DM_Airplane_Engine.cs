using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    [RequireComponent(typeof(DM_Airplane_Fuel))]
    public class DM_Airplane_Engine : MonoBehaviour
    {
        #region Variables
        public float maxForce = 200f;
        public float maxRPM = 2550f;
        public float shutOffSpeed = 2f;
        public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Header("Propellers")]
        public DM_Airplane_Propellers propeller;

        private bool isShutOff = false;
        private float lastThrottleValue;
        private float finalShutoffThrottleValue;
        private DM_Airplane_Fuel fuel;
        #endregion

        #region Properties
        public bool ShutEngineOff
        {
            set { isShutOff = value; }
        }
        private float currentRPM;
        public float CurrentRPMS
        {
            get { return currentRPM; }
        }
        #endregion

        #region Builtin Methods
        private void Start()
        {
            if (!fuel)
            {
                fuel = GetComponent<DM_Airplane_Fuel>();
                if (fuel)
                {
                    fuel.InitFuel();
                }
            }
        }

        #endregion

        #region Custom Methods
        public Vector3 CalculateForce(float throttle)
        {
            float finalThrottle = Mathf.Clamp01(throttle);

            if (!isShutOff)
            {
                finalThrottle = powerCurve.Evaluate(finalThrottle);
                lastThrottleValue = finalThrottle;
            } else
            {
                lastThrottleValue -= Time.deltaTime * shutOffSpeed;
                lastThrottleValue = Mathf.Clamp01(lastThrottleValue);
                finalShutoffThrottleValue = powerCurve.Evaluate(lastThrottleValue);
                finalThrottle = finalShutoffThrottleValue;
            }

            currentRPM = finalThrottle * maxRPM;
            if (propeller)
            {
                propeller.HandlePropeller(currentRPM);
            }

            HandleFuel(finalThrottle);

            float finalPower = finalThrottle * maxForce;
            Vector3 finalForce = transform.forward * finalPower;


            return finalForce;
        }

        void HandleFuel(float throttleValue)
        {
            if (fuel)
            {
                fuel.UpdateFuel(throttleValue);
                if (fuel.CurrentFuel <= 0)
                {
                    isShutOff = false;
                }
            }
        }
        #endregion
    }
}