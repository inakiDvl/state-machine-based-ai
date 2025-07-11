using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    private TargetComponent targetComponent;

    private void Awake()
    {
        targetComponent = GetComponent<TargetComponent>();
    }

    private void Update()
    {
        if (!targetComponent.TryGetTarget(out Transform target))
            return;

        Vector3 directinToTarget = target.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directinToTarget.normalized);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
