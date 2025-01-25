using UnityEngine;

public static class MinerAnimatorData
{
    public static readonly int s_canMove = Animator.StringToHash(nameof(s_canMove));
    public static readonly int s_canPickUp = Animator.StringToHash(nameof(s_canPickUp));
    public static readonly int s_canPut = Animator.StringToHash(nameof(s_canPut));
    public static readonly int s_canWait = Animator.StringToHash(nameof(s_canWait));
    public static readonly int s_canBuild = Animator.StringToHash(nameof(s_canBuild));
}
