using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class DM_Audio : MonoBehaviour
    {
        #region Variables
        [Header("Airplane Audio Properties")]
        public DM_BaseAirplane_Input input;
        public AudioSource idleSource;
        public AudioSource fullThrottleSource;
        public float maxPitchValue = 1.2f;

        private float finalVolume;
        private float finalPitchValue;

        private bool isShutOff = false;
        public bool IsShutOff
        {
            set { isShutOff = value; }
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            if (fullThrottleSource)
            {
                fullThrottleSource.volume = 0f;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (input)
                HandleAudio();
        }

        #region Cusom Methods
        protected virtual void HandleAudio()
        {
            if (isShutOff)
            {
                finalVolume = Mathf.Lerp(finalVolume, 0, Time.deltaTime * 2f);
                idleSource.volume = finalVolume;

            } else
            {
                idleSource.volume = 0.25f;
                finalVolume = Mathf.Lerp(0f, 1f, input.StickyThrottle);
                finalPitchValue = Mathf.Lerp(1f, maxPitchValue, input.StickyThrottle);
            }

            if (fullThrottleSource)
            {
                fullThrottleSource.volume = finalVolume;
                fullThrottleSource.pitch = finalPitchValue;
            }
        }

        #endregion
    }

}