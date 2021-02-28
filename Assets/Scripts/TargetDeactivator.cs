using UnityEngine;

public class TargetDeactivator : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private IToggler _toggler;

    private void Start()
    {
        _toggler = target.GetComponent<IToggler>();
        if (!ReferenceEquals(_toggler, null)) return;
        
        Debug.LogError("TargetDeactivator's target must have component that implements IToggler");
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!string.Equals(other.tag, "Player")) return;

        _toggler.Deactivate();
    }
}