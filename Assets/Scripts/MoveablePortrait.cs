using System.Collections;
using UnityEngine;

public class MoveablePortrait : MonoBehaviour, IThreat
{
    [SerializeField] private int thingLevel;
    [SerializeField] private bool friendlyPortrait;

    public float duration { get; set; }
    public GameObject player;
    public bool friendly { get; set; }
    public int level { get; set; }
    private float speed = 0.2f;
    private Quaternion rotation1 = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion rotation2 = Quaternion.Euler(45f, 0f, 0f);
    public ThreatState state { get; set; }
    public float followSharpness = 0.1f;
    private float attackRange = 0.2f;
    private Vector3 offset;
    private Vector3 initialPos;

	private void Awake()
	{
        initialPos = gameObject.transform.position;
        offset = gameObject.transform.position - player.transform.position;
    }

	public void Init()
	{
        level = thingLevel;
        friendly = friendlyPortrait;
        duration = 2.0f;
        state = ThreatState.ACTIVE;
        if (friendly)
		{
            StartCoroutine(RotatePortrait());
        } else
		{
            StartCoroutine(PortraitFollowsPlayer());
		}
	}

    public IEnumerator RotatePortrait()
	{
        yield return new WaitForSeconds(Random.Range(duration, 5.0f));
        
        transform.rotation = Quaternion.Slerp(rotation1, rotation2, speed * Time.deltaTime);

        yield return new WaitForSeconds(duration);

        transform.rotation = Quaternion.Slerp(rotation2, rotation1, speed * Time.deltaTime);

        StartCoroutine(RotatePortrait());
    }

    public IEnumerator PortraitFollowsPlayer() {

        yield return new WaitForSeconds(duration);

        state = ThreatState.ACTIVE;

        if (player != null)
        {
            float blend = 1f - Mathf.Pow(1f - followSharpness, Time.deltaTime * 30f);

            transform.position = Vector3.Lerp(
                   transform.position,
                   player.transform.position + offset,
                   blend);

            yield return new WaitForSeconds(0.5f);
            transform.position = initialPos;
        }
        else
        {
            yield return null;
        }

        StartCoroutine(PortraitFollowsPlayer());
    }

    public void Deactivate()
	{
        if (friendly) { StopCoroutine(RotatePortrait()); } else { StopCoroutine(PortraitFollowsPlayer()); };
        state = ThreatState.INACTIVE;
        level += 1;
	}
}
