using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePortrait : MonoBehaviour, IThreat
{
    public float duration { get; set; }
    public float maxIntensity; 
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

	private void Update()
    {

        PortraitFollowsPlayer();
    }

    public void Init()
	{
        state = ThreatState.ACTIVE;
        if (friendly)
		{
            StartCoroutine(RotatePortrait());
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

    public void PortraitFollowsPlayer() {

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
			}
        }
        else
        {
            return;
        }

    }

    public void Deactivate()
	{
        StopCoroutine(RotatePortrait());
        state = ThreatState.INACTIVE;
	}
}
