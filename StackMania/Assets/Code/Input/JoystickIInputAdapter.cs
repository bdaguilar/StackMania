public class JoystickIInputAdapter : IInput
{
    private readonly JoyButton _joyButton;

    public JoystickIInputAdapter(JoyButton joyButton)
    {
        _joyButton = joyButton;
    }

    public bool IsStopActionPressed()
    {
        return _joyButton.IsPressed;
    }
}
