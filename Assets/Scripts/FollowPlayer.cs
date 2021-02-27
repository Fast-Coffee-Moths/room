using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public Vector3 offset = Vector3.zero;
    public Quaternion rotation = Quaternion.Euler(0, 0, 0);

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _player.transform.position + offset;
        this.transform.rotation = rotation;
    }
}
