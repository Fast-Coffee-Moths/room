using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    private GameObject _currentInteractableObject;

    private void OnTriggerEnter(Collider other)
    {
        InteractableObject obj = other.gameObject.GetComponent<InteractableObject>();
        if (obj != null)
        {
            _currentInteractableObject = other.gameObject;
            Debug.Log("Ready to interact with: " + other.gameObject.name);
        }
    }
}
