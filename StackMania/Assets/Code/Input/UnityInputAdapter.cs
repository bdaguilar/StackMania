using UnityEngine;

public class UnityInputAdapter : IInput
{
    public bool IsStopActionPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
