using UnityEngine;
using UnityEngine.AI;

public class IdleState : MonoBehaviour, IState
{
    [SerializeField] private string animationName;
    [SerializeField] private float animationBlendDuration = 0.15f;

    private Animator animator;
    
    private int animationNameHash;

    public void EnterState()
    {
        animator.CrossFadeInFixedTime(animationNameHash, animationBlendDuration);
    }

    public void UpdateState(float deltaTime) { }

    public void ExitState() {}

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationNameHash = Animator.StringToHash(animationName);
    }
}
