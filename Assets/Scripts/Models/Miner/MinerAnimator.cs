using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MinerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move() =>  _animator.SetTrigger(MinerAnimatorData.s_canMove);
    public void PickUpOre() => _animator.SetTrigger(MinerAnimatorData.s_canPickUp);
    public void PutOre() => _animator.SetTrigger(MinerAnimatorData.s_canPut);
    public void Build() => _animator.SetTrigger(MinerAnimatorData.s_canBuild);
    public void Wait()
    {
        _animator.ResetTrigger(MinerAnimatorData.s_canMove);
        _animator.SetTrigger(MinerAnimatorData.s_canWait);
    }
}
