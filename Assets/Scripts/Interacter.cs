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
            FollowPlayer container = GameObject.Find("DraggableObjectContainer").GetComponent<FollowPlayer>();
            GameObject bodyInteracter = GameObject.Find("BodyInteracter");
            container.offset = bodyInteracter.transform.position - transform.position;
            container.rotation = bodyInteracter.transform.rotation;
            _currentInteractableObject.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableObjectInterface obj = other.gameObject.GetComponent<InteractableObjectInterface>();
        if (obj != null)
        {
            _currentInteractableObject = obj;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _currentInteractableObject = null;
    }
}
