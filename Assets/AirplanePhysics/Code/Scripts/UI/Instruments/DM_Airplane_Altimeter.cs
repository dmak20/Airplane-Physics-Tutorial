using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class DM_Airplane_Altimeter : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Altimeter Properties")]
        public DM_Airplane_Controller airplane;
        public RectTransform hundredsPointer;
        public RectTransform thousandsPointer;
        #endregion


        #region BuiltIn Methods
        // Start is called before the first frame update

        #endregion

        #region Interface Methods
        public void HandleAirplaneUI()
        {
            if (airplane)
            {
                float currentAlt = airplane.CurrentMSL;
                float currentThousands = currentAlt / 1000f;
                currentThousands = Mathf.Clamp(currentThousands, 0, 10);

                float currentHundreds = currentAlt - (Mathf.Floor(currentThousands) * 1000f);
                currentHundreds = Mathf.Clamp(currentHundreds, 0, 1000);

                if (thousandsPointer)
                {
                    float normalizedThousands = Mathf.InverseLerp(0, 10f, currentThousands);
                    float thousandsRotation = 360 * normalizedThousands;
                    thousandsPointer.rotation = Quaternion.Euler(0, 0, -thousandsRotation);
                }

                if (hundredsPointer)
                {
                    float normalizedHundreds = Mathf.InverseLerp(0, 1000f, currentHundreds);
                    float hundredsRotation = 360 * normalizedHundreds;
                    hundredsPointer.rotation = Quaternion.Euler(0,0, -hundredsRotation);
                }

            }
        } 
        #endregion
    }

}