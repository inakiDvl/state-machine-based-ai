using UnityEngine;
using UnityEngine.AI;

public class AttackState : MonoBehaviour, IState
{
    [SerializeField] private int attackEndEventId;
    [SerializeField] private string animatinName;
    [SerializeField] private float animationBlendDuration = 0.15f;
    [SerializeField] private float cooldownDuration = 1f;
    [SerializeField] private float attackRange = 1f;

    private AnimationEventReciver animationEventReciver;

    private Animator animator;
    private NavMeshAgent agent;

    private float agentSpeed;
    private int animationNameHash;
    
    private bool attacking;
    private float cooldownEndTime = -1f;

    public float GetAttackRange()
    {
        return attackRange;
    }

    public bool IsPerforming()
    { 
        return attacking;
    }

    public bool CanAttack()
    {
        return !attacking && Time.time >= cooldownEndTime;
    }

    public void EnterState()
    {
        attacking = true;
        animator.CrossFadeInFixedTime(animationNameHash, animationBlendDuration);
        agent.speed = 0;
        cooldownEndTime = -1;
    }

    public void UpdateState(float deltaTime) { }

    public void ExitState()
    {
        agent.speed = agentSpeed;
    }

    private void EndAttack(int eventId)
    {
        if (attackEndEventId != eventId)
            return;
            
        cooldownEndTime = Time.time + cooldownDuration;
        attacking = false;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agentSpeed = agent.speed;
        animationEventReciver = GetComponent<AnimationEventReciver>();
        animationNameHash = Animator.StringToHash(animatinName);
    }

    private void OnEnable()
    {
        animationEventReciver.OnEventIdRecived += EndAttack;
    }

    private void OnDisable()
    {
        animationEventReciver.OnEventIdRecived -= EndAttack;
    }
}
