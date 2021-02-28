using UnityEngine;


public class TargetOnTriggerFromReferenceActivator : MonoBehaviour
{
    [SerializeField] private GameObject reference;
    [SerializeField] private GameObject target;

    private IToggler _toggler;
    
    private void Start()
    {
        _toggler = target.GetComponent<IToggler>();
        if (!ReferenceEquals(_toggler, null)) return;
        
        Debug.LogError("TargetOnTriggerFromReferenceActivator's target must have component that implements IToggler");
        enabled = false;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (!enabled) return;
        if (ReferenceEquals(_toggler, null)) {
            Debug.LogErrorFormat("TargetOnTriggerFromReferenceActivator target's toggler cannot be null");

            return;
        }

        GameObject gameObject = other.gameObject;
        if (reference.GetInstanceID() != gameObject.GetInstanceID()) {
            gameObject = gameObject.transform.parent.gameObject;
        }

        if (reference.GetInstanceID() != gameObject.GetInstanceID()) return;

        _toggler.Activate();

        enabled = false;
    }
}
