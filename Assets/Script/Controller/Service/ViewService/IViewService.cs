using UnityEngine;

public interface IViewService {
	Canvas Canvas { get; }
    void Init(IServices services);
    IView SetView(ViewTypes type);
    void CreateTopPanel(IServices services);
    void CreateBottomPanel(IServices services);
}
