using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : MonoBehaviour, InteractableObjectInterface
{
    private GameObject _currentTarget;
    private bool _interacted = false;

    public bool IsInteracting()
    {
        return _interacted;
    }

    public void Interact()
    {
        ToggleInteraction();
    }

    public void ToggleInteraction()
    {
        if (_interacted)
        {
            _interacted = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            ChangeLayerMaskForAllChildren("Default", 0);
        } else
        {
            _interacted = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            ChangeLayerMaskForAllChildren("PickedUpObject", 12);
        }
    }

    void ChangeLayerMaskForAllChildren(string layerName, int layerID)
    {
        foreach (Transform child in transform)
        {
            ChangeLayerMask(child, "layerName", layerID);
        }
    }

    void ChangeLayerMask(Transform obj, string layerName, int layerID)
    {
        if (layerName != null)
        {
            obj.gameObject.layer = layerID;
        }
    }

    void Update()
    {
        if (_interacted)
        {
            transform.rotation = _currentTarget.transform.parent.gameObject.transform.rotation;
            this.transform.position = _currentTarget.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentTarget = other.gameObject;
    }
}
