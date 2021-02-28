using System.Collections;
using UnityEngine.Assertions;
using UnityEngine;

public class SpawnableShowHideThing : MonoBehaviour, IThreat
{
    [SerializeField] private int thingLevel;
    [SerializeField] private bool friendlyThing;

    [SerializeField]
    private Transform[] spawnPositions;

    [SerializeField]
    private ParticleSystem explosionPrefab;

    public GameObject player;
    public float duration { get; set; } 
    public bool friendly { get; set; }
    public int level { get; set; }
    public ThreatState state { get; set; }
    private bool show = true;
    private float attackRange;

    public void  Init()
	{
        Assert.IsNotNull(spawnPositions);

        thingLevel = level;
        friendlyThing = friendly;

        if (show)
		{
            StartCoroutine(Show());
		}
        else
		{
            StartCoroutine(Hide());
		}
	}

    public void Deactivate()
	{
        StopAllCoroutines();
        gameObject.SetActive(false);
        level += 1;
	}

    private IEnumerator Show()
	{
        show = true;

        yield return new WaitForSeconds(duration + 2/level);

        transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;

        gameObject.SetActive(true);

        if (!friendly && Vector3.Distance(transform.position, player.transform.position) < attackRange)
		{
            Explode();
		}

        yield return new WaitForSeconds(duration + 2/level);

        StartCoroutine(Hide());
	}

    private IEnumerator Hide()
	{
        show = false;
        gameObject.SetActive(false);
        yield return new WaitForSeconds(duration + 2/level);
        StartCoroutine(Show());
    }

    private void Explode()
    {
        explosionPrefab.Play();
        //AudioManager plays explosion SFX
        //Player dies and respawns
        Deactivate();
    }
}
