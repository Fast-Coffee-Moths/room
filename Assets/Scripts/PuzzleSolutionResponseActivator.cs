using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PuzzleSolutionResponseActivator : MonoBehaviour, IToggler
{
    private Collider collider;
    [SerializeField] private float delay;

    private bool _active;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    public bool IsActive()
    {
        return _active;
    }

    public void Activate()
    {
        if (_active) return;

        StartCoroutine(Activation());
    }

    private IEnumerator Activation()
    {
        _active = true;

        yield return new WaitForSeconds(delay);

        collider.enabled = true;
    }

    public void Deactivate() { }

    public void Toggle()
    {
        Activate();
    }
}
