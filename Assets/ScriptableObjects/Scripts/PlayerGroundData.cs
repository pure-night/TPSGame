using System;
using UnityEngine;

[Serializable]
public class PlayerGroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 3f;
    [field: SerializeField][field: Range(0f, 25f)] public float Speed { get; private set; } = 8f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 8f;
    [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }

    [field: Header("IdleData")]

    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 1f;

    [field: Header("RunData")]
    [field: SerializeField][field: Range(0f, 4f)] public float RunSpeedModifier { get; private set; } = 2f;
}
