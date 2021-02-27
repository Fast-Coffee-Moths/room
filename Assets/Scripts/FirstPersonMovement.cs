using Data;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public PlayerData playerData;

    private Vector2 _velocity;

    private void FixedUpdate()
    {
        _velocity.y = Input.GetAxis("Vertical") * playerData.speed * Time.fixedDeltaTime;
        _velocity.x = Input.GetAxis("Horizontal") * playerData.speed * Time.fixedDeltaTime;

        transform.Translate(_velocity.x, 0, _velocity.y);
    }
}
