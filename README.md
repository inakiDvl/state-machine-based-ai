# AI with Sate Machines
State machine pattern for character AI.

The AI has two main components:
1. The states; defined by an interface.
2. And a class that controls the transition between the states.

In this example we have three states:
1. IdleState
2. ChaseState
3. AttackState

And two state controllers or the AI itself:
1. ChaseAI: It transitons between chase and idle.
2. AttackAI: It transitions between attack, chase and idle.

The ChaseAI in action:

![alt text](chase.gif)

The AttackAI in action:

![alt text](attack.gif)

# How it's done
Its a component based architecture.

Components:
- TargetComponent
- RotateToTarget
- AttackState
- ChaseState
- IdleState
- State Machine or AI

The State Machine or AI its the component that manages the transition between states.
To transition to a new state, first make sure the current state its not the next state, then it must exit the previous state, set the current stete as the next state and finally enter the current state:

```cs
private void ChangeState(IState nextState)
{
    if (currentState == nextState)
        return;

    currentState.ExitState();
    currentState = nextState;
    currentState.EnterState();
}
```

The IState interface:
```cs
public interface IState
{
    public void EnterState();
    public void UpdateState(float deltaTime);
    public void ExitState();
}
```

Example of the transitions in AttackAI:

```cs
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
```cs