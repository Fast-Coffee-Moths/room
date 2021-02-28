using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingThing : MonoBehaviour, IThreat
{
    public float duration { get; set; } 
    public bool friendly { get; set; }
    public int level { get; set; }
    public ThreatState state { get; set; }
    private bool activated;
    private int _nEncounters;

    private GameObject player;

    // User Inputs
    [SerializeField] private float _floatSinamplitude = 0.5f;
    [SerializeField] private float _floatSinFrequency = 1f;
    [SerializeField] private float _enemyChaseSpeed = 1f;

    public void Init()
	{
        state = ThreatState.ACTIVE;
        gameObject.SetActive(true);
        activated = true;
        friendly = true;
        _nEncounters = 0;
        player = GameObject.Find("Player");
	}

    private void Update()
    {
        if (activated && !friendly)
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
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        } else
        {
            Events.SimpleEventSystem.TriggerEvent("player-loss-event");
        }
    }
}
