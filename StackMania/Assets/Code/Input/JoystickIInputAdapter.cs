public class JoystickIInputAdapter : IInput
{
    private readonly JoyButton _joyButton;

    public JoystickIInputAdapter(JoyButton joyButton)
    {
        _joyButton = joyButton;
    }

    public bool IsStopActionPressed()
    {
        throw new System.NotImplementedException();
    }
}
