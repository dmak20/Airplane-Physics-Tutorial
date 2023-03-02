using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    [RequireComponent(typeof(WheelCollider))]
    public class DM_Airplane_Wheel : MonoBehaviour
    {
        #region Variables
        private WheelCollider WheelCol;
        #endregion

        #region Builtin Methods
        private void Start()
        {
            WheelCol = GetComponent<WheelCollider>();
        }
        #endregion

        #region Custom Methods
        public void InitWheel()
        {
            if(WheelCol)
            {
                WheelCol.motorTorque = 0.000000000001f;
            }
        }
        #endregion

    }
}