using Events;
using UnityEngine;

public class PlayerDeathOnTriggerEnterEmitter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!enabled || !string.Equals(other.tag, "Player")) return;
        
        SimpleEventSystem.TriggerEvent(Events.Types.PlayerLossEvent);
        enabled = false;
    }
}
