using UnityEngine;
using UnityEngine.AI;

public class AttackState : StateMachineBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _player;
    private float _time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.transform.GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player");
        _time = 0f;
        _agent.isStopped = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _time += Time.deltaTime;
        float distance = Vector3.Distance(_agent.transform.position, _player.transform.position);
        if (_time >= 0.75f && distance < 2f)
        {
            _player.GetComponent<DeadController>().PlayerDie();
        }

        if (_time >= 1.5f)
        {
            animator.SetBool("Stun", false);
            _time = 0f;
        }
    }
}
