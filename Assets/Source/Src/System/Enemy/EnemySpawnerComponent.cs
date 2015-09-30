using Entitas;
using System.Xml;

public class EnemySpawnerComponent : IComponent {
	public int level;

	public bool used;
	public XmlNode node;
}