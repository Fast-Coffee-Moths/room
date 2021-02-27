using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public float sensitivity = 1;
    public float smoothing = 2;

    [SerializeField] private Transform character;

    private Vector2 _currentMouseLook;
    private Vector2 _appliedMouseDelta;

    private void Start()
    {
        if (character == null) {
            Debug.LogError("FirstPersonLook requires character transform to be set");
            enabled = false;

            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get smooth mouse look.
        Vector2 smoothMouseDelta = Vector2.Scale(
            new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")),
            sensitivity * smoothing * Vector2.one);
        _appliedMouseDelta = Vector2.Lerp(_appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
        _currentMouseLook += _appliedMouseDelta;
        _currentMouseLook.y = Mathf.Clamp(_currentMouseLook.y, -90, 90);

        // Rotate camera and controller.
        transform.localRotation = Quaternion.AngleAxis(-_currentMouseLook.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(_currentMouseLook.x, Vector3.up);
    }

}
