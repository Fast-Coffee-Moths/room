using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneOnPlayerDeathReloader : MonoBehaviour
{
    [SerializeField] private string scene;
    
    private void OnEnable()
    {
        SimpleEventSystem.AddListener(Events.Types.PlayerDeathEffectFinishedEvent, ReloadScene);
    }

    private void OnDisable()
    {
        SimpleEventSystem.RemoveListener(Events.Types.PlayerDeathEffectFinishedEvent, ReloadScene);
    }

    private void ReloadScene()
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
