using Data;
using Events;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    private InteractableObjectInterface _currentInteractableObject;
    [SerializeField] private PlayerData playerData;
    private float _originalSpeedValue;
    private bool _isDraggable = false;
    private bool _isDragging = false;
    [SerializeField] float _speedWhileDraggingObject = 1f;

    private void Start()
    {
        _originalSpeedValue = playerData.speed;
    }

    private void OnEnable()
    {
        SimpleEventSystem.AddListener(Events.Types.InteractableDemandsInteractionCeasesEvent, StopCurrentInteraction);
    }

    private void OnDisable()
    {
        SimpleEventSystem.RemoveListener(Events.Types.InteractableDemandsInteractionCeasesEvent, StopCurrentInteraction);
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
            playerData.speed = _originalSpeedValue;
            _isDragging = false;
        } else
        {
            playerData.speed = _speedWhileDraggingObject;
            _isDragging = true;
        }
    }

    private void StopCurrentInteraction()
    {
        if (!ReferenceEquals(_currentInteractableObject, null) && _currentInteractableObject.IsInteracting())
            _currentInteractableObject.Interact();
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
        StopCurrentInteraction();
        _currentInteractableObject = null;
        _isDraggable = false;
        playerData.speed = _originalSpeedValue;
    }
}
