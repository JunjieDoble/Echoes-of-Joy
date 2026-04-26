using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseAlways : MonoBehaviour
{
    [Header("Chase Settings")]
    [SerializeField] private float updateRate = 0.1f;

    private Transform _player;
    private NavMeshAgent _agent;
    private Animator _animator;

    private float _timer;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;

        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_player == null) return;

        _timer += Time.deltaTime;

        if (_timer >= updateRate)
        {
            _timer = 0f;

            _agent.SetDestination(_player.position);
        }

        if (_animator != null)
        {
            _animator.SetFloat("Speed", _agent.velocity.magnitude);
        }
    }
}