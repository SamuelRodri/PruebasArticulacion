using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour.Controllers;
using TFG.Behaviour.VR;
using UnityEngine;

namespace TFG.Behaviour.Column
{
    // Class that represents objects joined by joints and control their rotation
    public class JointComponent : MonoBehaviour
    {
        // Previous and Next component
        public JointComponent prev;
        public JointComponent next;

        private Vector3 rotation;
        public Transform cube;
        public Quaternion cubeOffset;
        public Vector3 cubePosOffset;

        private Vector3 initialPosition;
        private Quaternion initialRotation;

        public float initialDistancePrev;
        public float initialDistanceNext;

        public bool prevJointActivated;
        public bool nextJointActivated;
        public bool areFlexible;

        private void Awake()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;

            //if (!gameObject.name.Equals("Axis (C2)")) return;
            //cubeOffset = Quaternion.Inverse(cube.rotation) * transform.rotation;
        }

        private void Start()
        {
            SimulationController.OnRestore += ResetTransform;
        }

        public void SetPrev(JointComponent p)
        {
            prev = p;
            initialDistancePrev = Vector3.Distance(transform.position, prev.transform.position);
        }

        public void SetNext(JointComponent n)
        {
            next = n;
            initialDistanceNext = Vector3.Distance(transform.position, next.transform.position);
        }

        public bool HasPrev() { return prev != null; }
        public bool HasNext() { return next != null; }

        public void ResetTransform()
        {
            transform.SetPositionAndRotation(initialPosition, initialRotation);
        }

        private void Update()
        {
            if (GetComponent<XROffsetGrabInteractable>().isGrabbed) // The object is beign grabed
            {
                var rot = GetComponent<XROffsetGrabInteractable>().interactor.transform.rotation * GetComponent<XROffsetGrabInteractable>().offset;
                //rotation = CheckHardLimits(rot).eulerAngles;
                rotation = rot.eulerAngles;
                transform.rotation = Quaternion.Euler(rotation);
            }

            ////Comentar cuando se ejecute en las gafas
            //if (gameObject.name.Equals("Axis (C2)"))
            //{
            //    var rot = (cube.rotation * cubeOffset);
            //    rotation = CheckHardLimits(rot).eulerAngles;
            //}
            else
            {
                if (SimulationController.areJointsActivated || prevJointActivated || nextJointActivated)
                {
                    #region Rotation
                    Quaternion prevRot = transform.rotation;
                    Quaternion nextRot = transform.rotation;

                    
                    if (HasPrev() && (prevJointActivated || SimulationController.areJointsActivated)) { prevRot = FollowComponentRotation(prev); }
                    if (HasNext() && (nextJointActivated || SimulationController.areJointsActivated)) { nextRot = FollowComponentRotation(next); }
                    
                    rotation = Quaternion.Slerp(prevRot, nextRot, 0.5f).eulerAngles;

                    transform.rotation = Quaternion.Euler(rotation);
                    #endregion
                }
            }
        }

        // Returns the rotation neccesary to follow a component
        private Quaternion FollowComponentRotation(JointComponent jc)
        {
            Vector3 rotation = transform.rotation.eulerAngles;

            var angleX = Vector3.SignedAngle(transform.right, jc.transform.right, Vector3.right);
            var angleY = Vector3.SignedAngle(transform.up, jc.transform.up, Vector3.up);
            var angleZ = Vector3.SignedAngle(transform.forward, jc.transform.forward, Vector3.forward);

            // Tolerance
            if (Mathf.Abs(angleX) > 2 || Mathf.Abs(angleY) > 2 || Mathf.Abs(angleZ) > 1)
            {
                rotation = Quaternion.Slerp(transform.rotation, jc.transform.rotation, 0.5f).eulerAngles;
            }

            return Quaternion.Euler(rotation);
        }
    }
}