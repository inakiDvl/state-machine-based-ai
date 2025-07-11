using UnityEngine;

public class AnimationEventTriggerer : StateMachineBehaviour
{
    [SerializeField] private string description;
    [SerializeField] private int id;
    [SerializeField] private float normalizedTriggerTime;

    private bool triggered;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        triggered = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float currentTime = stateInfo.normalizedTime % 1;
        
        if (currentTime < normalizedTriggerTime)
            triggered = false;
            
        if (!triggered && currentTime >= normalizedTriggerTime)
            TriggerEvent(animator);
    }

    private void TriggerEvent(Animator animator)
    {
        AnimationEventReciver eventContoller = animator.GetComponent<AnimationEventReciver>();
        eventContoller.PassEventId(id);
        triggered = true;
    }
}
