public interface IPhase
{
	void SetEventService(EventService service);
	void CreateActions();
	void Execute();
}

