using System.Collections;
using Events;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DeathFadeOutEffectSystem : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    [SerializeField] private PostProcessVolume volume;
    [SerializeField] private float effectTime;
    [SerializeField] private Color baseMaterialColor;
    [SerializeField] private AnimationCurve fadeOutIntensityOverTime;
    [SerializeField] private AnimationCurve cameraDistortionIntensityOverTime;

    private Material _fadeOutMaterial;
    private Coroutine _current;

    private void Start()
    {
        if (ReferenceEquals(volume, null)) {
            Debug.LogError("DeathFadeOutEffectSystem's volume is not set to a PostProcessingVolume");
            enabled = false;

            return;
        }

        MeshRenderer r = GetComponent<MeshRenderer>();
        if (ReferenceEquals(r, null)) {
            Debug.LogError("DeathFadeOutEffectSystem needs to have a mesh renderer");
            enabled = false;

            return;
        }
        if (ReferenceEquals(r.material, null)) {
            Debug.LogError("DeathFadeOutEffectSystem MeshRenderer does not have a material");
            enabled = false;

            return;
        }

        _fadeOutMaterial = r.material;
        baseMaterialColor = _fadeOutMaterial.GetColor(EmissionColor);

        ResetDeathVisualEffect();
    }

    private void OnEnable()
    {
        SimpleEventSystem.AddListener(Events.Types.PlayerLossEvent, TriggerDeathVisualEffect);
        SimpleEventSystem.AddListener(Events.Types.PlayerDeathEffectResetEvent, ResetDeathVisualEffect);
    }

    private void OnDisable()
    {
        SimpleEventSystem.RemoveListener(Events.Types.PlayerLossEvent, TriggerDeathVisualEffect);
        SimpleEventSystem.RemoveListener(Events.Types.PlayerDeathEffectResetEvent, ResetDeathVisualEffect);
    }

    private void TriggerDeathVisualEffect()
    {
        _current = StartCoroutine(DeathVisualEffect());
    }

    private void ResetDeathVisualEffect()
    {
        if (!ReferenceEquals(_current, null)) StopCoroutine(_current);

        float currentIntensity = fadeOutIntensityOverTime.Evaluate(0);

        _fadeOutMaterial.SetColor(EmissionColor, baseMaterialColor * Mathf.Pow(2, currentIntensity));
        volume.weight = cameraDistortionIntensityOverTime.Evaluate(0);
    }

    private IEnumerator DeathVisualEffect()
    {
        float startTime = Time.time, currentDelta = 0;

        while (currentDelta < 1f) {
            yield return new WaitForEndOfFrame();

            currentDelta = (Time.time - startTime) / effectTime;
            float currentIntensity = fadeOutIntensityOverTime.Evaluate(currentDelta);

            _fadeOutMaterial.SetColor(EmissionColor, baseMaterialColor * Mathf.Pow(2, currentIntensity));
            volume.weight = cameraDistortionIntensityOverTime.Evaluate(currentDelta);
        }

        SimpleEventSystem.TriggerEvent(Events.Types.PlayerDeathEffectFinishedEvent);

        yield return null;
    }
}
