using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotTweenAnimationCustomRotate : MonoBehaviour
{
    [Tooltip("AXIS TO ROTATE")] public AxisRotation axisToRotate;
    [Tooltip("VECTOR TO ROTATE")] public float targetToSpinValue;
    [Tooltip("NO NEED TO FILL IN IF TARGET IS ITSELF")] public GameObject target;
    [Tooltip("SPEED TO ROTATE")] public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 _finalTarget = Vector3.zero;
        switch (axisToRotate)
        {
            case AxisRotation.X:
                _finalTarget = new Vector3(targetToSpinValue, this.transform.rotation.y, this.transform.rotation.z);
                break;

            case AxisRotation.Y:
                _finalTarget = new Vector3(this.transform.rotation.x, targetToSpinValue, this.transform.rotation.z);
                break;

            case AxisRotation.Z:
                _finalTarget = new Vector3(this.transform.rotation.x, this.transform.rotation.y, targetToSpinValue);
                break;

        }


        if (target == null) { target = this.gameObject; }

        target.transform.DOLocalRotate(_finalTarget, speed, RotateMode.Fast).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

}

public enum AxisRotation
{
    X,
    Y,
    Z,
}
