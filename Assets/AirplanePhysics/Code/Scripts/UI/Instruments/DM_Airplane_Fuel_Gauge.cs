using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class DM_Airplane_Fuel_Gauge : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Fuel Gauge Properties")]
        public DM_Airplane_Fuel fuel;
        public RectTransform pointer;
        public Vector2 minMaxRotation = new Vector2(-90, 90);
        #endregion

        #region Custom Methods
        public void HandleAirplaneUI()
        {
            if (fuel && pointer)
            {
                float wantedRotation = Mathf.Lerp(minMaxRotation.x, minMaxRotation.y, fuel.NormalizedFuel);
                pointer.rotation = Quaternion.Euler(0, 0, -wantedRotation);
            }
        }
        #endregion

    }
}