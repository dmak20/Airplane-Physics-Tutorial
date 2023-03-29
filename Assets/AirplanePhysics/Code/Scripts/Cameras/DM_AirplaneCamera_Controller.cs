using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class DM_AirplaneCamera_Controller : MonoBehaviour
    {
        #region Variables
        [Header("Camera Controller Properties")]
        public DM_BaseAirplane_Input input;
        public int startCameraIndex = 0;
        public List<Camera> cameras = new List<Camera>();

        private int cameraIndex = 0;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            if (startCameraIndex >= 0 && startCameraIndex < cameras.Count)
            {
                DisableAllCameras();
                cameras[startCameraIndex].enabled = true;
                cameras[startCameraIndex].GetComponent<AudioListener>().enabled = true;

            }
        }

        // Update is called once per frame
        void Update()
        {
            if (input)
            {
                if (input.CameraSwitch)
                {
                    SwitchCamera();
                }
            }
        }
        #endregion

        #region Custom Methods
        protected virtual void SwitchCamera()
        {
            if (cameras.Count > 0)
            {
                DisableAllCameras();
                cameraIndex++;

                if (cameraIndex >= cameras.Count) { cameraIndex = 0; }

                cameras[cameraIndex].enabled = true;
                cameras[cameraIndex].GetComponent<AudioListener>().enabled = true;
            }
        }

        void DisableAllCameras()
        {
            foreach (Camera c in cameras)
            {
                c.enabled = false;
                c.GetComponent<AudioListener>().enabled = false;
            }   
        }
        #endregion
    }

}