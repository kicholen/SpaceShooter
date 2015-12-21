using Entitas;

public interface ITweenAccessor
{
	int GetValues(IComponent target, int tweenType, float[] returnValues);
	void SetValues(IComponent target, int tweenType, float[] newValues);
}