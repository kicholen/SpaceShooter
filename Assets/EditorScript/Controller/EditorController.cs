using System.Reflection;
using UnityEngine;

public class EditorController : MonoBehaviour, IController {

	IServices services;
	public IServices Services {
		get {
			return services;
		}
	}

    public GameObject GameObject { get { return gameObject; } }
    public MaterialStorage MaterialStorage { get { return GetComponent<MaterialStorage>(); } }

    void Start () {
		services = new EditorServices(this);
        replaceFactoriesWithEditorOnes();
        services.Init();
    }
	
	void Update () {
		services.Update();
	}

    void replaceFactoriesWithEditorOnes() {
        ViewService viewService = services.ViewService as ViewService;
        FieldInfo fieldInfo = viewService.GetType().GetField("factory", BindingFlags.Instance | BindingFlags.NonPublic);
        fieldInfo.SetValue(viewService, new EditorViewFactory(Services));
    }

	/*public void SetPathCreator() {
		pathCreator = GameObject.FindGameObjectsWithTag("PathCreator")[0].GetComponent<PathCreator>();
		pathCreator.SetGameObjectsFromLoadedOne();
	}*/
}