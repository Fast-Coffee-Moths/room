using System.Collections;
using UnityEngine;

public class TwoOrientationTransformToggler : MonoBehaviour, IToggler
{
    [SerializeField] private Transform target;

    [SerializeField] private Transform inactiveState;
    [SerializeField] private Transform activeState;

    [SerializeField] private float activationTime;
    [SerializeField] private float inactivationTime;

    private bool _state;
    private float _delta;
    private Coroutine _current;

    public bool IsActive()
    {
        return _state;
    }

    public void Toggle()
    {
        if (ReferenceEquals(_current, null)) StopCoroutine(_current);

        _current = StartCoroutine(_state ? Deactivation() : Activation());
    }

    public void Activate()
    {
        if (_state) return;
        if (!ReferenceEquals(_current, null)) StopCoroutine(_current);

        _current = StartCoroutine(Activation());
    }

    public void Deactivate()
    {
        if (!_state) return;
        if (!ReferenceEquals(_current, null)) StopCoroutine(_current);

        _current = StartCoroutine(Deactivation());
    }

    private IEnumerator Activation()
    {
        _state = true;

        while (_delta < 1f) {
            yield return new WaitForEndOfFrame();

            _delta += Time.deltaTime / activationTime;
            target.rotation = Quaternion.Slerp(inactiveState.rotation, activeState.rotation, _delta);
        }

        _delta = 1;

        yield return null;
    }
    
    private IEnumerator Deactivation()
    {
        _state = false;

        while (_delta > 0) {
            yield return new WaitForEndOfFrame();

            target.rotation = Quaternion.Slerp(inactiveState.rotation, activeState.rotation, _delta);
            _delta -= Time.deltaTime / inactivationTime;
        }

        _delta = 0;

        yield return null;
    }
}
