using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class DM_Airplane_Propellers : MonoBehaviour
    {
        #region Variables
        [Header("Propeller Properties")]
        public float minRotationRPM = 30f;
        public float minBlurRPMs = 300;
        public float minTextureSwap = 600f;
        public GameObject mainProp;
        public GameObject blurredProp;

        [Header("Material Properties")]
        public Material blurredPropMat;
        public Texture2D blurLevel1;
        public Texture2D blurLevel2;
        #endregion

        #region Builtin Methods
        private void Start()
        {
            if (mainProp && blurredProp)
                HandleSwapping(0);
        }
        #endregion

        #region Custom Methods
        public void HandlePropeller(float currentRPM)
        {
            float dps = ((currentRPM * 360) / 60) * Time.deltaTime;
            dps = Mathf.Clamp(dps, 0f, minRotationRPM);
            transform.Rotate(Vector3.forward, dps);

            if (mainProp && blurredProp)
                HandleSwapping(currentRPM);
        }

        private void HandleSwapping(float currentRPM)
        {
            if (currentRPM > minBlurRPMs)
            {
                blurredProp.SetActive(true);
                mainProp.SetActive(false);

                if (blurredProp && blurLevel1 && blurLevel2)
                {
                    if (currentRPM > minTextureSwap)
                        blurredPropMat.SetTexture("_MainTex", blurLevel2);
                    else
                       blurredPropMat.SetTexture("_MainTex", blurLevel1);
                }
            } else
            {
                blurredProp.SetActive(false);
                mainProp.SetActive(true);
            }
        }
        #endregion


    }
}