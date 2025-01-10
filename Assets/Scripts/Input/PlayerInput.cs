using UnityEngine;

public class PlayerInput
{
    private const string ForwardBackward = nameof(ForwardBackward);
    private const string UpDown = nameof(UpDown);
    private const string LeftRight = nameof(LeftRight);
    private const string HorizontalRotation = nameof(HorizontalRotation);

    public float VelocityOfForwardBackward => Input.GetAxis(ForwardBackward);
    public float VelocityOfUpDown => Input.GetAxis(UpDown);
    public float VelocityOfLeftRight => Input.GetAxis(LeftRight);
    public float VelocityOfHorizontalRotation => Input.GetAxis(HorizontalRotation);
}
