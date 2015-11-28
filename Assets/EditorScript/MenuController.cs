using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class MenuController : MonoBehaviour {

	public GameObject listGO;
	Transform content;

	void Start () {
		listGO = Instantiate(Resources.Load<GameObject>("EditorPrefab/List")).gameObject;
		content = listGO.transform.FindChild("Scroll View/Viewport/Content");

		setButtons();

	}

	void Update () {

	}

	void setButtons() {
		#if !UNITY_WEBPLAYER && !UNITY_WEBGL
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath + "/Resources/");
		FileInfo[] fileInfos = directoryInfo.GetFiles();
		string lfThis = typeof(PathModelComponent).Name;

		List<string> paths = new List<string>();
		foreach (FileInfo fileInfo in fileInfos) {
			if (fileInfo.Name.StartsWith(lfThis) && fileInfo.Name.IndexOf(".xml") > 0 && fileInfo.Name.IndexOf("meta") < 0) {
				paths.Add(fileInfo.Name);
			}
		}
		string[] pathsArray = new string[paths.Count];
		pathsArray = paths.ToArray();
		IComparer comparer = new PathComparer();
		Array.Sort(pathsArray, comparer);
		paths.Clear();
		paths.AddRange(pathsArray);

		foreach (string path in paths) {
			addButton(path);
		}
		#endif
	}

	void addButton(string name) {
		GameObject go = Instantiate(Resources.Load<GameObject>("EditorPrefab/Button")).gameObject;
		go.name = name;
		go.transform.SetParent(content.transform);
		go.GetComponent<Button>().onClick.AddListener(onPathChosen);
		go.transform.FindChild("Text").GetComponent<Text>().text = name;

		string thumbnailPath = "/Resources/" + name.Replace(".xml", ".png");
		if (File.Exists(Application.dataPath + thumbnailPath)) {
			go.transform.FindChild("Thumbnail").GetComponent<Image>().sprite = Resources.Load<Sprite>(name.Split('.')[0]);
		}
	}

	void onPathChosen() {
		string xmlToLoad = EventSystem.current.currentSelectedGameObject.name;
		string sufix = xmlToLoad.Split('_')[1].Split('.')[0];
		EditorController.instance.component = Utils.Deserialize<PathModelComponent>(sufix);
		EditorController.instance.blockTouch = false;
		EditorController.instance.sufix = sufix;
		EditorController.instance.SetPathCreator();
		EditorController.instance.menuController = this;
		listGO.SetActive(false);
	}
}

internal class PathComparer : IComparer {

	public int Compare(object x, object y) {
		char[] seperator = new char[1];
		seperator[0] = '_';

		char[] secondSeperator = new char[1];
		secondSeperator[0] = '.';

		int valueX = Convert.ToInt16((x as string).Split(seperator)[1].Split(secondSeperator)[0]);
		int valueY = Convert.ToInt16((y as string).Split(seperator)[1].Split(secondSeperator)[0]);

		if (valueX < valueY) {
			return -1;
		}
		else if (valueX > valueY) {
			return 1;
		}

		return 0;
	}

}