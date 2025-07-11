using System;
using UnityEngine;

public class AnimationEventReciver : MonoBehaviour
{
    public event Action<int> OnEventIdRecived;

    public void PassEventId(int id)
    {
        OnEventIdRecived?.Invoke(id);
    }
}
