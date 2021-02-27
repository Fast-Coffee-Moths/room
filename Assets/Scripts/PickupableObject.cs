using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : MonoBehaviour, InteractableObjectInterface
{
    private GameObject _currentTarget;
    private bool _interacted = false;

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
        } else
        {
            _interacted = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void Update()
    {
        if (_interacted)
        {
            this.transform.position = _currentTarget.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentTarget = other.gameObject;
    }
}
