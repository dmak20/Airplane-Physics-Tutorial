using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    [RequireComponent(typeof(WheelCollider))]
    public class DM_Airplane_Wheel : MonoBehaviour
    {
        #region Variables
        [Header("Wheel Properties")]
        public Transform wheelGO;
        public bool isBrakingWheel = false;
        public float brakePower = 5f;
        
        private WheelCollider WheelCol;
        private Vector3 worldPos;
        private Quaternion worldRot;
        private float finalBrakeForce;

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

        public void HandleWheel(DM_BaseAirplane_Input input)
        {
            if (WheelCol)
            {
                WheelCol.GetWorldPose(out worldPos, out worldRot);
                if (wheelGO)
                {
                    wheelGO.rotation = worldRot;
                    wheelGO.position = worldPos;
                }

                if (input.Brake > 0.1f)
                {
                    finalBrakeForce = Mathf.Lerp(finalBrakeForce, input.Brake * brakePower, Time.deltaTime);
                    WheelCol.brakeTorque = finalBrakeForce;
                }
                else
                {
                    finalBrakeForce = 0;
                    WheelCol.motorTorque = 0.000000000001f;
                    WheelCol.brakeTorque = 0;
                }
            }
        }
        #endregion

    }
}