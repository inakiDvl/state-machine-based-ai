using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    [SerializeField] private Transform target;

    public Transform GetTarget()
    {
        return target;
    }

    public bool TryGetTarget(out Transform target)
    {
        target = this.target;
        return target != null;
    }
}
