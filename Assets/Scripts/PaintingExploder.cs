using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PaintingExploder : MonoBehaviour, IToggler
{
    [SerializeField] private float launchForce;
    [SerializeField] private Vector3 launchDirection;

    private Rigidbody _rigidbody;
    private bool _exploded;
    
    public bool IsActive()
    {
        return _exploded;
    }

    public void Activate()
    {
        if (_exploded) return;

        _rigidbody.isKinematic = false;
        _rigidbody.AddRelativeForce(launchDirection * launchForce, ForceMode.Impulse);
        _exploded = true;
    }

    public void Deactivate() {}

    public void Toggle()
    {
        Activate();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Reset()
    {
        launchDirection.Normalize();
    }
}
