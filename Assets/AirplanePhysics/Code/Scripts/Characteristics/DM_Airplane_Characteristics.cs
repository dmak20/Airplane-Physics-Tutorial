using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace DM
{
    public class DM_Airplane_Characteristics : MonoBehaviour
    {
        #region Variables
        [Header("Characteristics Properties")]
        public float forwardSpeed;
        public float mph;
        public float maxMPH = 110f;
        public float rbLerpSpeed = 0.01f;

        [Header("Lift Properties")]
        public float maxLiftPower = 800f;
        public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Drag Properties")]
        public float dragFactor = 0.01f;

        [Header("Control Properties")]
        public float pitchSpeed = 1000f;
        public float rollSpeed = 1000f;
        public float yawSpeed = 1000f;

        DM_BaseAirplane_Input input;
        private Rigidbody rb;
        private float startDrag;
        private float startAngularDrag;

        private float maxMPS;
        private float normalizedMPH;

        private float angleOfAttack;
        private float pitchAngle;
        private float rollAngle;
        #endregion

        #region Constants
        const float mpsToMph = 2.23694f;
        #endregion

        #region BuiltIn Methods
        #endregion

        #region Custom Methods
        public void InitCharacteristics(Rigidbody rigidBody, DM_BaseAirplane_Input input)
        {
            // Basic initialization
            rb = rigidBody;
            startDrag = rb.drag;
            startAngularDrag = rb.angularDrag;

            // Find the max meters per second
            maxMPS = maxMPH / mpsToMph;
            this.input = input;
        }

        public void UpdateCharacteristics()
        {
            if (rb)
            {
                // process the flight model
                CalculateForwardSpeed();
                CalculateLift();
                CalculateDrag();
                HandlePitch();
                HandleRoll();
                HandleYaw();
                HandleBanking();
                HandleRigidbodyTransform();
            }
        }


        void CalculateForwardSpeed()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            forwardSpeed = Mathf.Max(0f, localVelocity.z);
            forwardSpeed = Mathf.Clamp(forwardSpeed, 0, maxMPS);

            mph = forwardSpeed * mpsToMph;
            mph = Mathf.Clamp(mph, 0f, maxMPH);

            normalizedMPH = Mathf.InverseLerp(0f, maxMPH, mph);
        }

        void CalculateLift()
        {
            // Angle of attack
            angleOfAttack = Vector3.Dot(rb.velocity.normalized, transform.forward);
            angleOfAttack *= angleOfAttack;

            Vector3 liftDir = transform.up;
            float liftPower = liftCurve.Evaluate(normalizedMPH) * maxLiftPower;

            Vector3 finalLiftForce = liftDir * liftPower * angleOfAttack;
            rb.AddForce(finalLiftForce);
        }

        void CalculateDrag()
        {
            float speedDrag = forwardSpeed * dragFactor;
            float finalDrag = startDrag + speedDrag;

            rb.drag = finalDrag;
            rb.angularDrag = startAngularDrag * forwardSpeed;
        }

        private void HandleRigidbodyTransform()
        {
            if (rb.velocity.magnitude > 1f)
            {
                Vector3 updateVelocity = Vector3.Lerp(rb.velocity, transform.forward * forwardSpeed, forwardSpeed * angleOfAttack * Time.deltaTime * rbLerpSpeed);
                rb.velocity = updateVelocity;

                Quaternion updatedRotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(rb.velocity.normalized, transform.up), Time.deltaTime * rbLerpSpeed);
                rb.MoveRotation(updatedRotation);
            }
        }

        private void HandlePitch()
        {
            Vector3 flatForward = transform.forward;
            flatForward.y = 0f;
            pitchAngle = Vector3.Angle(transform.forward, flatForward);

            Vector3 pitchTorque = input.Pitch * pitchSpeed * transform.right;
            rb.AddTorque(pitchTorque);
        }
        private void HandleRoll()
        {
            Vector3 flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;
            rollAngle = Vector3.SignedAngle(transform.right, flatRight, transform.forward);

            Vector3 rollTorque = -input.Roll * rollSpeed * transform.forward;
            rb.AddTorque(rollTorque);
        }

        private void HandleYaw()
        {
            Vector3 yawTorque = input.Yaw * yawSpeed * transform.up;
            rb.AddTorque(yawTorque);
        }

        private void HandleBanking()
        {
            float bankSide = Mathf.InverseLerp(-90, 90, rollAngle);
            float bankAmount = Mathf.Lerp(-1, 1, bankSide);
            Vector3 bankTorque = bankAmount * rollSpeed * transform.up;
            rb.AddTorque(bankTorque);
        }

        #endregion
    }
}