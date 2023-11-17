using System.Threading.Tasks;

public class StartBattleFromGameCommand : ICommand
{
    public async Task Execute()
    {
        ServiceLocator.Instance.GetService<GameStateController>().Reset();
        ServiceLocator.Instance.GetService<IScoreSystem>().Reset();
        ServiceLocator.Instance.GetService<ScoreView>().Reset();
        await Task.Yield();
    }
}