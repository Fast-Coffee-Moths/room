using Events;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PaintingResetter : MonoBehaviour, IInteractionFinalizer
{
    private Rigidbody _rigidbody;
    private Collider[] _colliders;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Collider _collider = GetComponent<Collider>();
        Collider[] childColliders = GetComponentsInChildren<Collider>();

        _colliders = new Collider[childColliders.Length + 1];
        for (int i = 0; i < _colliders.Length - 1; i++) {
            _colliders[i] = childColliders[i];
        }

        _colliders[_colliders.Length - 1] = _collider;
    }

    public void FinalizeInteraction()
    {
        _rigidbody.isKinematic = true;
        foreach (Collider collider in _colliders) {
            collider.enabled = false;
        }

        SimpleEventSystem.TriggerEvent(Events.Types.InteractableDemandsInteractionCeasesEvent);
    }
}
