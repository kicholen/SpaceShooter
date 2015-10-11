using Entitas;

public class BonusModelComponent : IComponent {
	public int type;
	public int minAmount;
	public int maxAmount;
	public float probability;
	public string resource;
}