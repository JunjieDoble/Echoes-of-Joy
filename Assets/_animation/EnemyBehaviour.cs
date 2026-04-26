using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float detectionAngle = 180f;

    private Animator _animator;
    private Transform _player;
    private Color _gizmosColor = Color.green;

    private bool _alwaysChaseMode = false;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_player == null) return;

        float distance = GetDistanceToPlayer();
        _animator.SetFloat("DistanceToPlayer", distance);

        if (_alwaysChaseMode)
        {
            _animator.SetBool("SeePlayer", true);
            _gizmosColor = Color.magenta;

            MoveTowardsPlayer();
        }
        else
        {
            CheckNormalVision();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (_player == null) return;

        Vector3 directionToPlayer = (_player.position - transform.position).normalized;
        directionToPlayer.y = 0;

        transform.position += directionToPlayer * speed * Time.deltaTime;

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

    private void CheckNormalVision()
    {
        float horizontalDistance = GetDistanceToPlayer();
        float angleToPlayer = GetAngleToPlayer();

        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 directionToPlayer = (_player.position - origin).normalized;

        RaycastHit hit;
        bool seesPlayer = false;

        if (Physics.Raycast(origin, directionToPlayer, out hit, detectionRadius))
        {
            if (hit.collider.CompareTag("Player"))
            {
                seesPlayer = true;
            }
        }

        bool inRange = horizontalDistance <= detectionRadius;
        bool inAngle = angleToPlayer <= detectionAngle * 0.5f;

        if (seesPlayer && inRange && inAngle)
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

    public void ActivateAlwaysChase()
    {
        _alwaysChaseMode = true;
        Debug.Log($"{gameObject.name}: Modo Always Chase ACTIVADO - Persiguiendo al jugador sin pausa.");
    }

    public void DeactivateAlwaysChase()
    {
        _alwaysChaseMode = false;
        Debug.Log($"{gameObject.name}: Modo Always Chase DESACTIVADO - Volviendo a comportamiento normal.");
    }

    public void StunEnemy()
    {
        _animator.SetBool("Stun", true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (_player != null)
        {
            Vector3 origin = transform.position + Vector3.up * 1.5f;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(origin, _player.position);
        }
    }

    private float GetDistanceToPlayer()
    {
        Vector3 playerPos = _player.position;
        Vector3 enemyPos = transform.position;

        Vector2 playerXZ = new Vector2(playerPos.x, playerPos.z);
        Vector2 enemyXZ = new Vector2(enemyPos.x, enemyPos.z);

        return Vector2.Distance(playerXZ, enemyXZ);
    }

    private float GetAngleToPlayer()
    {
        Vector3 directionToPlayer = (_player.position - transform.position).normalized;
        return Vector3.Angle(transform.forward, directionToPlayer);
    }

    public Transform[] GetPatrolPoints()
    {
        return patrolPoints;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void AlertClown()
    {
        detectionAngle = 360f;
        detectionRadius = 20f;
    }

    public void UnAlertClown()
    {
        detectionAngle = 180f;
        detectionRadius = 10f;
    }
}