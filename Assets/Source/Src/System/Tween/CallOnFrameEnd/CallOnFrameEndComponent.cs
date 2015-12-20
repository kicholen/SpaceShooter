using Entitas;
using System;

public class CallOnFrameEndComponent : IComponent {
	public Action<Entity> callback;
}