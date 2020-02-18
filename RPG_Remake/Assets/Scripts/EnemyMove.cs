using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask;

    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();
    }

    public void OnDetectObject(Collider collider)
    {
        if (!_status.IsMovable)
        {
            _agent.isStopped = true;
            return;
        }

        if (collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position;
            var distance = positionDiff.magnitude; 
            var direction = positionDiff.normalized; 

            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance, raycastLayerMask);
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