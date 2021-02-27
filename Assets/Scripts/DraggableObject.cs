using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour, InteractableObjectInterface
{
    private GameObject _currentTarget;
    private bool _interacted = false;
    private Vector3 _distance;
    private GameObject _container;

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
        }
        else
        {
            _interacted = true;
            Vector3 _distance = new Vector3(_container.transform.position.x, transform.position.y, _container.transform.position.y) - new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    void Update()
    {
        if (_interacted)
        {
            transform.position = new Vector3(_container.transform.position.x, transform.position.y, _container.transform.position.z) + _distance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentTarget = other.gameObject;
    }
}
