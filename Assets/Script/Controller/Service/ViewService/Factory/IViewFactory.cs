public interface IViewFactory {
	IView Create(ViewTypes type);
	void Init(IServices services);
}