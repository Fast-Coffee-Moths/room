using System.Collections;
using UnityEngine;

public class FlashingLights : MonoBehaviour, IThreat
{
    [SerializeField] private int lightLevel;
    public float duration { get; set; } // The total of seconds the flash wil last
    public float maxIntensity; // The maximum intensity the flash will reach
    public Light myLight;        // light
    public bool friendly { get; set; }
    public int level { get; set; }
    public ThreatState state { get; set; }

	public void Init()
	{
        level = lightLevel;
        state = ThreatState.ACTIVE;
        StartCoroutine(FlashNow());
    }

    public IEnumerator FlashNow()
    {

        float waitTime = 0.2f;

        while (myLight.intensity < maxIntensity)
        {
            myLight.intensity += Time.deltaTime / waitTime;        // Increase intensity
            yield return null;
        }
        while (myLight.intensity > 0)
        {
            myLight.intensity -= Time.deltaTime / waitTime;        //Decrease intensity
            yield return null;
        }

        StartCoroutine(FlashNow());
    }

    public void Deactivate()
	{
        StopCoroutine(FlashNow());
        state = ThreatState.INACTIVE;
        level += 1;
	}
}
