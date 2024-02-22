using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string speedParameterName = "Speed";
    [SerializeField] private string directionParameterName = "Direction";
    [SerializeField] private string landParameterName = "Land";

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";
    
    [SerializeField] private string dieParameterName = "Die";
    
    public int GroundParameterHash { get; private set; }
    public int SpeedParameterHash { get; private set; }
    public int DirectionParameterHash { get; private set; }
    public int LandParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }
    
    public int DieParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        SpeedParameterHash = Animator.StringToHash(speedParameterName);
        DirectionParameterHash = Animator.StringToHash(directionParameterName);
        LandParameterHash = Animator.StringToHash(landParameterName);

        AirParameterHash = Animator.StringToHash(airParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        FallParameterHash = Animator.StringToHash(fallParameterName);
        
        DieParameterHash = Animator.StringToHash(dieParameterName);
    }
}
