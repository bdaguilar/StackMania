using System;
using System.Threading.Tasks;

public class StopGameCommand : ICommand
{
    public async Task Execute()
    {
        await Task.Yield();
    }
}
