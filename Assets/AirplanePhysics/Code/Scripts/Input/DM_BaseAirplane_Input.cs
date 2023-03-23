using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class DM_BaseAirplane_Input : MonoBehaviour
    {
        #region Variables
        protected float pitch = 0f;
        protected float roll = 0f;
        protected float yaw = 0f;
        protected float throttle = 0f;

        public KeyCode brakeKey = KeyCode.Space;
        protected float brake = 0f;

        public int maxFlapIncrements = 2;
        protected int flaps = 1;

        public float throttleSpeed = 0.1f;

        private float stickyThrottle;
        public float StickyThrottle { get { return stickyThrottle; } }

        #endregion

        #region Properties
        public float Pitch { get { return pitch; }  }
        public float Roll { get { return roll; }  }
        public float Yaw { get { return yaw; }  }
        public float Throttle { get { return throttle; }  }
        public int Flaps { get { return flaps; }  }
        public float Brake { get { return brake; }  }
        #endregion


        #region Builtin Methdos
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }
        #endregion

        #region Custom Methods
        void HandleInput()
        {
            pitch = Input.GetAxis("Vertical");
            roll = Input.GetAxis("Horizontal");
            yaw = Input.GetAxis("Yaw");
            throttle = Input.GetAxis("Throttle");
            StickyThrottleControl();
            
            brake = Input.GetKey(brakeKey) ? 1 : 0;
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                flaps += 1;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                flaps -= 1;
            }

            flaps = Mathf.Clamp(flaps, 0, maxFlapIncrements);
        }

        void StickyThrottleControl()
        {
            stickyThrottle = stickyThrottle + (throttle * throttleSpeed * Time.deltaTime);
            stickyThrottle = Mathf.Clamp01(stickyThrottle);
        }

        #endregion
    }
}