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
    private float attackRange = 2f;

	public void  Init()
	{
        InvokeRepeating("Show", 1f, Random.Range(1, 5));
	}

    public void Deactivate()
	{
        CancelInvoke("Show");
        gameObject.SetActive(false);
        level += 1;
	}

    private void Show()
    {
        transform.position = spawnPositions[Random.Range(0, spawnPositions.Length - 1)].position;

        if (!friendly && Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            Explode();
        }

    }

    private void Explode()
    {
        explosionPrefab.Play();
        //AudioManager plays explosion SFX
        //Player dies and respawns
        Deactivate();
    }
}
