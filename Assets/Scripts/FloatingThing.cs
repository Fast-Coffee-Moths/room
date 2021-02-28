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

    public GameObject player;

    // User Inputs
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float _enemyChaseSpeed = 1f;
    private Vector3 offset;

    // Position Storage Variables
    Vector3 posOffset = new Vector3(0f, 3f, 0f);
    Vector3 tempPos = new Vector3();

    private float attackRange = 5.0f;

	private void Start()
	{
        Init();
	}

	public void Init()
	{
        level = thingLevel;
        friendly = friendlyThing;
        state = ThreatState.ACTIVE;
        gameObject.SetActive(true);
        _hasReachedPlayer = false;
        _nEncounters = 0;
        activated = true;
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        Debug.Log("activated: " + activated);
        Debug.Log("friendly: " + friendly);
        Debug.Log("reached: " + _hasReachedPlayer);
        if (activated && !friendly && !_hasReachedPlayer)
        {
            Debug.Log("chase!");
            // chase player
            transform.LookAt(player.transform);
            friendly = false;
            gameObject.GetComponent<SphereCollider>().radius = 0.5f;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            transform.Find("Collider").GetComponent<SphereCollider>().enabled = false;
            Vector3 floati = new Vector3(0, Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude, 0);

            transform.position += (transform.forward + floati) * _enemyChaseSpeed * Time.deltaTime;
        }
    }
    
    public void Deactivate()
	{
        activated = false;
        state = ThreatState.INACTIVE;
    }

    private void OnTriggerEnter(Collider other)
    {
        _nEncounters += 1;
        if (_nEncounters < 3)
        {
            Events.SimpleEventSystem.TriggerEvent("move-enemy-to-random-position");
        }
        else if (_nEncounters == 3)
        {
            // chase player
            friendly = false;
            gameObject.GetComponent<SphereCollider>().radius = 0.5f;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            transform.Find("Collider").GetComponent<SphereCollider>().enabled = false;
        }
        else
        {
            _hasReachedPlayer = true;
            Events.SimpleEventSystem.TriggerEvent("player-loss-event");
        }
    }

}
