using UnityEngine;

public class FloatingThing : MonoBehaviour, IThreat
{
    [SerializeField] private int thingLevel;
    [SerializeField] private bool friendlyThing = true;
    public float duration { get; set; } 
    public bool friendly { get; set; }
    public int level { get; set; }
    public ThreatState state { get; set; }
    private bool activated;
    private int _nEncounters;
    private bool _hasReachedPlayer;

    [SerializeField]
    private ParticleSystem explosionPrefab;
    private GameObject player;

    // User Inputs
    [SerializeField] private float _floatSinamplitude = 0.5f;
    [SerializeField] private float _floatSinFrequency = 1f;
    [SerializeField] private float _enemyChaseSpeed = 1f;

    public void Init()
	{
        thingLevel = level;
        friendly = friendlyThing;
        state = ThreatState.ACTIVE;
        gameObject.SetActive(true);
        activated = true;
        _hasReachedPlayer = false;
        _nEncounters = 0;
        player = GameObject.Find("Player");
	}

    private void Update()
    {
        if (activated && !friendly && !_hasReachedPlayer)
		{
            // chase player
            transform.LookAt(player.transform);

            Vector3 floati = new Vector3(0, Mathf.Sin(Time.fixedTime * Mathf.PI * _floatSinFrequency) * _floatSinamplitude, 0);

            transform.position += (transform.forward + floati) * _enemyChaseSpeed * Time.deltaTime;
        }
    }
    
    public void Deactivate()
	{
        activated = false;
        state = ThreatState.INACTIVE;
    }

    private void Explode()
    {
        explosionPrefab.Play();
        //AudioManager plays explosion SFX
        //Player dies and respawns
        gameObject.SetActive(false);
        Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        _nEncounters += 1;
        if (_nEncounters < 3)
        {
            Events.SimpleEventSystem.TriggerEvent("move-enemy-to-random-position");
        } else if (_nEncounters == 3)
        {
            // chase player
            friendly = false;
            gameObject.GetComponent<SphereCollider>().radius = 0.5f;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            transform.Find("PhysicsCollider").GetComponent<SphereCollider>().enabled = false;

        } else
        {
            _hasReachedPlayer = true;
            Events.SimpleEventSystem.TriggerEvent("player-loss-event");
        }
    }
}
