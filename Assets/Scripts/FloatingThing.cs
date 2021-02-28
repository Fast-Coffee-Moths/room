using UnityEngine;

public class FloatingThing : MonoBehaviour, IThreat
{
    [SerializeField] private int thingLevel;
    [SerializeField] private bool friendlyThing;
    public float duration { get; set; } 
    public bool friendly { get; set; }
    public int level { get; set; }
    public ThreatState state { get; set; }
    private bool activated;

    [SerializeField]
    private ParticleSystem explosionPrefab;

    public GameObject player;

    // User Inputs
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    private Vector3 offset;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private float attackRange = 5.0f;

	public void Init()
	{
        thingLevel = level;
        friendlyThing = friendly;
        state = ThreatState.ACTIVE;
        gameObject.SetActive(true);
        offset = this.gameObject.transform.position - player.transform.position;
        activated = true;
	}

    private void Update()
    {

        if (activated && friendly)
		{
            Float();

        } else if (activated && !friendly)
		{
            if (Vector3.Distance(transform.position - offset, player.transform.position) < attackRange)
			{
                Float();
                if (Random.Range(0, 100 / level) > 50)
                {
                    Explode();
                }
			}

		} else
		{
            return;
		}
        
    }
    
    public void Deactivate()
	{
        activated = false;
        state = ThreatState.INACTIVE;
    }

    private void Float()
	{
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    private void Explode()
	{
        explosionPrefab.Play();
        //AudioManager plays explosion SFX
        //Player dies and respawns
        gameObject.SetActive(false);
        Deactivate();
	}
}
