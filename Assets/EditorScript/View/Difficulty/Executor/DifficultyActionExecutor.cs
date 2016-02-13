public class DifficultyActionExecutor
{
    DifficultyModelComponent component;

    public DifficultyActionExecutor(DifficultyModelComponent component) {
        this.component = component;
    }

    public void Execute(IChangeDifficultyAction modifier) {
        modifier.Execute(component);
    }
}