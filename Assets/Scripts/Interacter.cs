using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    private InteractableObjectInterface _currentInteractableObject;
    private float _originalSpeedValue;
    private bool _isDraggable = false;
    private bool _isDragging = false;
    private FirstPersonMovement _movement;
    [SerializeField] float _speedWhileDraggingObject = 1f;

    private void Awake()
    {
        _movement = gameObject.GetComponent<FirstPersonMovement>();
        _originalSpeedValue = _movement.playerData.speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_currentInteractableObject != null)
            {
                if (_isDraggable)
                {
                    ToggleIsDragging();
                }
                _currentInteractableObject.Interact();
            }
        }
    }

    private void ToggleIsDragging()
    {
        if (_isDragging)
        {
            _movement.playerData.speed = _originalSpeedValue;
            _isDragging = false;
        } else
        {
            _movement.playerData.speed = _speedWhileDraggingObject;
            _isDragging = true;
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
            DraggableObject drag = other.gameObject.GetComponent<DraggableObject>();
            if (drag != null)
            {
                _isDraggable = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _currentInteractableObject = null;
        _isDraggable = false;
        _movement.playerData.speed = _originalSpeedValue;
    }
}
