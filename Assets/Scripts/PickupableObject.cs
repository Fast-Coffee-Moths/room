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
            ChangeLayerMaskForAllChildren(0);
        } else
        {
            _interacted = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            ChangeLayerMaskForAllChildren(12);
        }
    }

    void ChangeLayerMaskForAllChildren(int layerID)
    {
        foreach (Transform child in transform)
        {
            Debug.Log(child.transform.name);
            ChangeLayerMask(child, layerID);
        }
    }

    void ChangeLayerMask(Transform obj, int layerID)
    {
        obj.gameObject.layer = layerID;
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
