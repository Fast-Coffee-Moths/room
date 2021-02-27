using System;
using System.Collections;
using UnityEngine;

public class InteractionFinalizerTransformer : MonoBehaviour
{
    [Range(-1, 1)] [SerializeField] private float minimumOrientationDot;
    [SerializeField] private GameObject target;
    [SerializeField] private AnimationCurve closenessOverTime;
    private IInteractionFinalizer _finalizer;

    private bool IsOrientationValid(Quaternion orientation)
    {
        // check if it's sufficiently aligned
        return Quaternion.Dot(orientation, transform.rotation) >= minimumOrientationDot;
    }

    private IEnumerator UpdateTargetTransform()
    {
        Transform targetTransform = target.transform;

        Vector3    startPosition = targetTransform.position, endPosition = transform.position;
        Quaternion startRotation = targetTransform.rotation, endRotation = transform.rotation;
        float      startTime     = Time.time;

        // if there's no closeness evaluator, there's nothing we can do
        if (closenessOverTime.length == 0) {
            yield return null;
        }

        float currentCloseness = closenessOverTime.keys[0].value,
              targetCloseness  = closenessOverTime.keys[closenessOverTime.length - 1].value;
        while (Math.Abs(currentCloseness - targetCloseness) > Mathf.Epsilon) {
            currentCloseness = closenessOverTime.Evaluate(Time.time - startTime);
            targetTransform.position = Vector3.Lerp(startPosition, endPosition, currentCloseness);
            targetTransform.rotation = Quaternion.Slerp(startRotation, endRotation, currentCloseness);

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    private void Start()
    {
        Reset();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!enabled) return;
        if (ReferenceEquals(_finalizer, null)) {
            Debug.LogErrorFormat("target finalizer cannot be null");

            return;
        }

        if (target.GetInstanceID() != other.gameObject.GetInstanceID() || !IsOrientationValid(other.transform.rotation)) return;

        _finalizer.FinalizeInteraction();
        StartCoroutine(UpdateTargetTransform());

        enabled = false;
    }

    private void Reset()
    {
        if (ReferenceEquals(target, null)) {
            Debug.LogErrorFormat("target finalizer cannot be null");

            return;
        }

        _finalizer = target.GetComponent<IInteractionFinalizer>();
        if (ReferenceEquals(_finalizer, null))
            Debug.LogErrorFormat("collider \"{0}\" (\"{1}\") does not implement IInteractionFinalizer", target.name, target.tag);
    }
}
