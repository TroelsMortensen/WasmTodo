namespace BlazorWASM.StateContainers;

public class CounterStateContainer
{
    public Action<int> OnChange { get; set; }

    private int count = 0;

    public void Increment()
    {
        count++;
        OnChange?.Invoke(count);
    }
}