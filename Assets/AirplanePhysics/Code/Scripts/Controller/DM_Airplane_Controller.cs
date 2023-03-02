using DM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class DM_Airplane_Controller : DM_BaseRigidbody_Controller
    {
        #region
        [Header("Base Airplane Properties")]
        public DM_BaseAirplane_Input input;
        public Transform centerOfGravity;

        [Tooltip("Weight is in LBS")]
        public float airplaneWeight = 800f;

        [Header("Engines")]
        public List<DM_Airplane_Engine> engines = new List<DM_Airplane_Engine>();

        [Header("Wheels")]
        public List<DM_Airplane_Wheel> wheels = new List<DM_Airplane_Wheel>();
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
                HandleAerodynamics();
                HandleSteering();
                HandleBrakes();
                HandleAltitude();
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
                        rb.AddForce(engine.CalculateForce(input.Throttle));
                    }
                }
            }
        }

        void HandleAerodynamics()
        {

        }

        void HandleSteering()
        {

        }

        private void HandleAltitude()
        {

        }

        private void HandleBrakes()
        {

        }
        #endregion

    }
}