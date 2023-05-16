using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using UnityEngine;

namespace TFG.Utils
{
    public class RenameBone : MonoBehaviour
    {
        public GameObject parent;

        public void Rename()
        {
            name = parent.name + " - " + name;
        }

        public void AddJointComponent()
        {
            var initialTransform = transform;

            transform.gameObject.AddComponent<JointComponent>();
            
            transform.position = initialTransform.position;
            transform.rotation = initialTransform.rotation;
            transform.localScale = initialTransform.localScale;
            
        }
    }
}