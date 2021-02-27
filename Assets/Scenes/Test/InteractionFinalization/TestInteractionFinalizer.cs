using UnityEngine;

namespace Scenes.Test.InteractionFinalization
{
    public class TestInteractionFinalizer : MonoBehaviour, IInteractionFinalizer
    {
        public void FinalizeInteraction()
        {
            transform.parent = null;
        }
    }
}