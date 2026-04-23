using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float detectionAngle = 180f;

    private Animator _animator;
    private GameObject _player;
    private Color _gizmosColor = Color.green;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DistanceAndVisionToPlayer();
    }

    private void DistanceAndVisionToPlayer()
    {
        float HorizontalDistance = GetDistanceToPlayer();
        _animator.SetFloat("DistanceToPlayer", HorizontalDistance);

        float angleToPlayer = GetAngleToPlayer();

        RaycastHit hit;
        Vector3 directionToPlayer = _player.transform.position - transform.position;
        Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRadius);
        if (hit.collider != null && hit.collider.gameObject == _player && angleToPlayer <= detectionAngle / 2f && HorizontalDistance <= detectionRadius)
        {
            _animator.SetBool("SeePlayer", true);
            _gizmosColor = Color.red;
        }
        else
        {
            _animator.SetBool("SeePlayer", false);
            _gizmosColor = Color.green;
        }
    }

    public void StunEnemy()
    {
        _animator.SetBool("Stun", true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawRay(transform.position, transform.forward * detectionRadius);
    }

    private float GetDistanceToPlayer()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 enemyPos = transform.position;

        Vector2 playerXZ = new Vector2(playerPos.x, playerPos.z);
        Vector2 EnemyXZ = new Vector2(enemyPos.x, enemyPos.z);
        return Vector2.Distance(playerXZ, EnemyXZ);
    }

    private float GetAngleToPlayer()
    {
        Vector3 directionToPlayer = _player.transform.position - transform.position;
        Vector3 directionForward = transform.forward;
        return Vector3.Angle(directionForward, directionToPlayer);
    }

    public Transform[] GetPatrolPoints()
    {
        return patrolPoints;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
