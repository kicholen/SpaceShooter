public interface IViewFactoryService {
	IView CreateView(ViewTypes type);
	void Init(IServices services);
}