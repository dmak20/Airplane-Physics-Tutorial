using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DM
{
    public class DM_Airplane_Fuel : MonoBehaviour
    {
        #region Variables
        [Header("Fuel Properties")]
        [Tooltip("The total number of gallons in the fuel tank.")]
        public float fuelCapacity = 26f;
        [Tooltip("The average fuel burn rate per hour.")]
        public float fuelBurnRate = 6.1f;

        [Header("Events")]
        public UnityEvent onFuelFull = new UnityEvent();
        #endregion

        #region Properties
        private float normalizedFuel;
        public float NormalizedFuel
        {
            get { return normalizedFuel; }
        }

        private float currentFuel;
        public float CurrentFuel
        {
            get { return currentFuel; }
        }
        #endregion

        #region Custom Methods
        public void InitFuel()
        {
            currentFuel = fuelCapacity;
        }

        public void UpdateFuel(float aPercentage)
        {
            Debug.Log("Updating the fuel");
            float currentBurn = ((fuelBurnRate * aPercentage) / 3600) * Time.deltaTime;
            currentFuel -= currentBurn;
            currentFuel = Mathf.Clamp(currentFuel, 0, fuelCapacity);
            normalizedFuel = currentFuel / fuelCapacity;
        }

        public void AddFuel(float aFuelAmount)
        {
            currentFuel += aFuelAmount;
            currentFuel = Mathf.Clamp(currentFuel, 0, fuelCapacity);

            if (currentFuel >= fuelCapacity)
            {
                if (onFuelFull != null)
                {
                    onFuelFull.Invoke();

                }
            }
        }

        public void ResetFuel()
        {
            currentFuel = fuelCapacity;
        }

        #endregion
    }

}