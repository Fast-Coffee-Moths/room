using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePortrait : MonoBehaviour, IThreat
{
    [SerializeField] private int thingLevel;
    [SerializeField] private bool friendlyPortrait;

    public float duration { get; set; }
    public GameObject[] portraits;
    public GameObject player;
    public bool friendly { get; set; }
    public int level { get; set; }
    private float speed = 0.2f;
    private Quaternion rotation1 = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion rotation2 = Quaternion.Euler(45f, 0f, 0f);
    public ThreatState state { get; set; }
    public float followSharpness = 0.1f;
    private float attackRange = 3f;
    private Vector3 offset;

	private void Awake()
	{
        offset = this.gameObject.transform.position - player.transform.position;
    }

    public void Init()
	{
        thingLevel = level;
        friendlyPortrait = friendly;
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
        yield return new WaitForSeconds(duration);

        int index = Random.Range(0, portraits.Length);
        
        portraits[index].transform.rotation = Quaternion.Slerp(rotation1, rotation2, speed * Time.deltaTime);

        duration /= level;

        yield return new WaitForSeconds(duration);

        portraits[index].transform.rotation = Quaternion.Slerp(rotation2, rotation1, speed * Time.deltaTime);
    }

    public IEnumerator PortraitFollowsPlayer() {

        yield return new WaitForSeconds(duration);

        state = ThreatState.ACTIVE;

        if (!friendly && player != null)
        {
            float blend = 1f - Mathf.Pow(1f - followSharpness, Time.deltaTime * 30f);

            transform.position = Vector3.Lerp(
                   transform.position,
                   player.transform.position + offset,
                   blend);

            if (Vector3.Distance(transform.position - offset, player.transform.position) < attackRange)
			{
                //Player was attacked. Respawn, call enemy or do other stuff

                Deactivate();
            }
        }
        else
        {
            yield return null;
        }

    }

    public void Deactivate()
	{
        if (friendly) { StopCoroutine(RotatePortrait()); } else { StopCoroutine(PortraitFollowsPlayer()); };
        state = ThreatState.INACTIVE;
        level += 1;
	}
}
