using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuocAnh.Tool 
{
    public class HIngeJointGenerator : MonoBehaviour
    {
        public GameObject[] targets;

        public void CreateHingeJoint()
        {
            if(targets.Length < 0) { Debug.Log("NO TARGETS");return; }

            for (int i = 0; i < targets.Length; i++)
            {
                GameObject hinge = new GameObject(targets[i].name + "_HINGEJOINT");
                hinge.AddComponent<HingeJoint>();
                hinge.transform.parent = targets[i].transform;
                hinge.transform.localPosition = Vector3.zero;
            }
        }
    }
}


