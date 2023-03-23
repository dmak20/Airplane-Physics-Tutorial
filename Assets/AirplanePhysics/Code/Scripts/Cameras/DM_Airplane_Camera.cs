using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class DM_Airplane_Camera : DM_Basic_Follow_Camera
    {

        #region Variables
        [Header("Airplane Camera Properties")]
        public float minHeightFromGround = 2f;
        #endregion

        protected override void HandleCamera()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if (hit.distance < minHeightFromGround && hit.transform.tag == "ground")
                {
                    float wantedHeight = origHeight + minHeightFromGround - hit.distance;
                    height = wantedHeight;
                }
            }

            base.HandleCamera();


        }

    }
}