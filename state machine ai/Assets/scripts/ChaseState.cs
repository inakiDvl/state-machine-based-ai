using UnityEngine;
using UnityEngine.AI;

public class ChaseState : MonoBehaviour, IState
{
    [SerializeField] private string animationName;
    [SerializeField] private float animationBlendDuration = 0.15f;

    private TargetComponent targetComponent;

    private Animator animator;
    private NavMeshAgent agent;

    private int animationNameHash;

    public void EnterState()
    {
        animator.CrossFadeInFixedTime(animationNameHash, animationBlendDuration);
    }

    public void UpdateState(float deltaTime)
    {
        agent.SetDestination(targetComponent.GetTarget().position);
    }

    public void ExitState() {}

    private void Awake()
    {
        targetComponent = GetComponent<TargetComponent>();
        animator = GetComponent<Animator>();
        animationNameHash = Animator.StringToHash(animationName);
        agent = GetComponent<NavMeshAgent>();
    }
}
