using UnityEngine;
using UnityEngine.AI;

public class AttackAi : MonoBehaviour
{
    private NavMeshAgent agent;

    private TargetComponent targetComponent;

    private AttackState attackState;
    private ChaseState chaseState;
    private IdleState idleState;

    private IState currentState;

    private void HandleTransitions()
    {
        if (!targetComponent.TryGetTarget(out Transform _))
        {
            ChangeState(idleState);
            return;
        }

        float distanceFromTarget = Vector3.Distance(transform.position, targetComponent.GetTarget().position);

        if (attackState.IsPerforming())
            return;

        if (attackState.CanAttack() && distanceFromTarget <= attackState.GetAttackRange())
        {
            ChangeState(attackState);
            return;
        }

        if (distanceFromTarget > agent.stoppingDistance)
        {
            ChangeState(chaseState);
            return;
        }

        ChangeState(idleState);
    }

    private void ChangeState(IState nextState)
    {
        if (currentState == nextState)
            return;

        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
        Debug.Log(nextState);
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    
        targetComponent = GetComponent<TargetComponent>();
        attackState = GetComponent<AttackState>();
        chaseState = GetComponent<ChaseState>();
        idleState = GetComponent<IdleState>();
    }

    private void Start()
    {
        currentState = idleState;
        currentState.EnterState();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        HandleTransitions();
        currentState.UpdateState(deltaTime);
    }
}
