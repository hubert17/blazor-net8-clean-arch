namespace BlazorNet8CleanArch.Infrastructure.Handlers.BlazorStateHandler;

using BlazorState;
using System.Threading;
using System.Threading.Tasks;

public class CounterState : State<CounterState>
{
    public int Count { get; private set; }
    public override void Initialize()
    {
        Count = 0;
    }
    
    public record IncrementCount(int Count = 1) : IAction;
    private class IncrementCountHandler(IStore store) : ActionHandler<IncrementCount>(store)
    {
        CounterState CounterState => Store.GetState<CounterState>();

        public override Task Handle(IncrementCount action, CancellationToken cancellationToken)
        {
            CounterState.Count += action.Count;
            return Task.CompletedTask;
        }
    }
}

