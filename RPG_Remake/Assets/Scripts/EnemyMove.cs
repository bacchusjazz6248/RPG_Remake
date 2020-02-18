using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void OnDetectObject(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position; 
            var distance = positionDiff.magnitude; 
            var direction = positionDiff.normalized;

            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance);
            Debug.Log("hitCount: " + hitCount);
            if (hitCount == 0)
            {
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                _agent.isStopped = true;
            }
        }
    }
}