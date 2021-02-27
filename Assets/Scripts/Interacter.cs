using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    private InteractableObjectInterface _currentInteractableObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_currentInteractableObject != null)
            {
                _currentInteractableObject.Interact();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentInteractableObject == null)
        {
            InteractableObjectInterface obj = other.gameObject.GetComponent<InteractableObjectInterface>();
            if (obj != null)
            {
                _currentInteractableObject = obj;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _currentInteractableObject = null;
    }
}
