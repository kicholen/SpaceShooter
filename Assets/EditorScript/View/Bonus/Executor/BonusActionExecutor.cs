public class BonusActionExecutor
{
    BonusModelComponent component;

    public BonusActionExecutor(BonusModelComponent component) {
        this.component = component;
    }

    public void Execute(IChangeBonusAction modifier) {
        modifier.Execute(component);
    }
}