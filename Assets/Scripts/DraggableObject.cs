using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour, InteractableObjectInterface
{
    private GameObject _currentTarget;
    private bool _interacted = false;
    private GameObject _container;

    public bool IsInteracting()
    {
        return _interacted;
    }

    public void Interact()
    {
        _container = GameObject.Find("DraggableObjectContainer");
        ToggleInteraction();
    }

    public void ToggleInteraction()
    {
        if (_interacted)
        {
            _interacted = false;
            transform.parent = null;
        }
        else
        {
            _interacted = true;
            transform.parent = _container.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentTarget = other.gameObject;
    }
}
