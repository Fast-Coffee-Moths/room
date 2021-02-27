using Events;
using UnityEngine;

public class TestDeathEffectTrigger : MonoBehaviour
{
    private bool _currentTriggerState;

    private void Update()
    {
        bool trigger = Input.GetButtonDown("Jump");
        if (!trigger) return;

        _currentTriggerState = !_currentTriggerState;
        SimpleEventSystem.TriggerEvent(_currentTriggerState
            ? Events.Types.PlayerLossEvent
            : Events.Types.PlayerDeathEffectResetEvent);
    }
}
