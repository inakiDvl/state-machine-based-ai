using UnityEngine;
using UnityEngine.AI;

public class ChaseAi : MonoBehaviour
{
    private NavMeshAgent agent;

    private TargetComponent targetComponent;

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

        if (distanceFromTarget >= agent.stoppingDistance)
        {
            ChangeState(chaseState);
        }
        else
        {
            ChangeState(idleState);
        }
    }

    private void ChangeState(IState nextState)
    {
        if (currentState == nextState)
            return;

        currentState.ExitState();
        currentState = nextState;
        currentState.EnterState();
    }

    private void Awake()
    {
        targetComponent = GetComponent<TargetComponent>();
        agent = GetComponent<NavMeshAgent>();
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
