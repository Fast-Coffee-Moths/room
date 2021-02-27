using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    OPENING,
    OPENED,
    CLOSING,
    CLOSED
}

public class Door : MonoBehaviour
{
    private State doorState = State.CLOSED;

    public State DoorState { get { return doorState; } }

    private float openTimer;
    private float closeTimer;
    private float autoCloseTimer;
  
    public bool blocked;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (doorState == State.OPENED)
        {

            if (blocked == false)
            {
                autoCloseTimer += Time.deltaTime;
            }
            else
            {
                autoCloseTimer = 0.0f;

            }
        }
        else
        {

            autoCloseTimer = 0.0f;
        }

        if (autoCloseTimer >= 5.0f && blocked == false)
        {
            ActivateDoor();
        }

        if (doorState == State.OPENING)
        {
            openTimer += Time.deltaTime;

        }
        if (openTimer >= 1.5f)
        {
            doorState = State.OPENED;

            ResetTimer();
        }
        if (doorState == State.CLOSING)
        {
            closeTimer += Time.deltaTime;
            blocked = true;
            ResetTimer();

        }
        if (closeTimer >= 1.5f)
        {
            doorState = State.CLOSED;

            ResetTimer();
        }
    }

    void OnTriggerExit()
    {
        blocked = true;
    }

    public void ActivateDoor()
    {
        if (doorState == State.CLOSED)
        {
            //Animation.Play(OpenDoor);
            doorState = State.OPENING;
            ResetTimer();
            return;
        }
        if (doorState == State.OPENING)
        {
            return;
        }
        if (doorState == State.CLOSING)
        { 
            ResetTimer();
            return;
        }
        if (doorState == State.OPENED && !blocked)
        {
            //AnimationManager.Play(CloseDoor);
            doorState = State.CLOSING;
            ResetTimer();
            return;
        }

    }

    void ResetTimer()
    {
        openTimer = 0.0f;
        closeTimer = 0.0f;
    }
}
