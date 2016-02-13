using Entitas;

public class DifficultyModelComponent : IComponent {
	public int id;
	public int type;
    public int hpBoostPercent;
	public int dmgBoostPercent;
	public int missileSpeedBoostPercent;
}