using DM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    [RequireComponent(typeof(DM_Airplane_Characteristics))]
    public class DM_Airplane_Controller : DM_BaseRigidbody_Controller
    {
        #region
        [Header("Base Airplane Properties")]
        public DM_BaseAirplane_Input input;
        public DM_Airplane_Characteristics characteristics;
        public Transform centerOfGravity;

        [Tooltip("Weight is in LBS")]
        public float airplaneWeight = 800f;

        [Header("Engines")]
        public List<DM_Airplane_Engine> engines = new List<DM_Airplane_Engine>();

        [Header("Wheels")]
        public List<DM_Airplane_Wheel> wheels = new List<DM_Airplane_Wheel>();

        [Header("Control Surfaces")]
        public List<DM_Airplane_ControlSurface> controlSurfaces = new List<DM_Airplane_ControlSurface> ();
        #endregion

        #region Constants
        const float poundsToKilos = 0.453592f;
        #endregion

        #region Builtin Methods
        public override void Start()
        {
            base.Start();

            float finalMass = airplaneWeight * poundsToKilos;
            if (rb)
            {
                rb.mass = finalMass;
                if (centerOfGravity)
                {
                    rb.centerOfMass = centerOfGravity.localPosition;
                }

                if (characteristics)
                {
                    characteristics.InitCharacteristics(rb, input);
                }
            }

            if (wheels != null)
            {
                if (wheels.Count > 0)
                {
                    foreach (DM_Airplane_Wheel wheel in wheels)
                    {
                        wheel.InitWheel();
                    }
                }
            }

  
        }
        #endregion

        #region Custom Methods
        protected override void HandlePhysics()
        {
            if (input)
            {
                HandleEngines();
                HandleCharacteristics();
                HandleWheel();
                HandleAltitude();
                HandleControlSurfaces();
            }
        }

        private void HandleWheel()
        {
            if (wheels.Count > 0)
            {
                foreach(DM_Airplane_Wheel wheel in wheels)
                {
                    wheel.HandleWheel(input);
                }
            }
        }

        private void HandleControlSurfaces()
        {
            if (controlSurfaces.Count > 0)
            {
                foreach (DM_Airplane_ControlSurface controlSurface in controlSurfaces)
                {
                    controlSurface.HandleControlSurface(input);
                }
            }
        }

        void HandleEngines()
        {
            if (engines != null)
            {
                if (engines.Count > 0)
                {
                    foreach (DM_Airplane_Engine engine in engines)
                    {
                        // add force for each engine
                        rb.AddForce(engine.CalculateForce(input.StickyThrottle));
                    }
                }
            }
        }

        void HandleCharacteristics()
        {
            if (characteristics)
            {
                characteristics.UpdateCharacteristics();
            }
        }

        private void HandleAltitude()
        {

        }

        #endregion

    }
}