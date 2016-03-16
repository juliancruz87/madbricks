using System.Collections;
using UnityEngine;

public class MovementSettings : ScriptableObject
{
    [SerializeField]
    private float collideDistance = 0.09f;
    [SerializeField]
    private float stickyDistance = 0.045f;
    [SerializeField]
    private float maxJumpDistance = 0.15f;

    public float CollideDistance
    {
        get
        {
            return collideDistance;
        }
    }

    public float StickyDistance
    {
        get
        {
            return stickyDistance;
        }
    }

    public float MaxJumpDistance
    {
        get
        {
            return maxJumpDistance;
        }
    }
}



