using UnityEngine;
using System.Xml;
#if !UNITY_WEBPLAYER && !UNITY_WEBGL
using System.IO;
#endif
using Newtonsoft.Json;

public static class Utils
{
	const string jsonSufix = ".json";

	public static XmlNode LoadXml(string path) {
		XmlDocument document = new XmlDocument();
		TextAsset textFile = Resources.Load<TextAsset>(path);
		document.LoadXml(textFile.text);
		return document.FirstChild;
	}


	public static T Deserialize<T>(string sufix = "") {
		string path = typeof(T).Name;
		if (sufix != "") {
			path += "_" + sufix;
		}
		#if UNITY_WEBPLAYER || UNITY_WEBGL
		if (PlayerPrefs.HasKey(path)) {
			T component = JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(path));
			return component;
		}
		else {
			TextAsset textFile = Resources.Load<TextAsset>(path);
			T component = JsonConvert.DeserializeObject<T>(textFile.text);
			return component;
		}
		#else
		JsonSerializer serializer = new JsonSerializer();
		if (File.Exists(Application.persistentDataPath + "/" + path + jsonSufix)) {
			TextReader streamReader = new StreamReader(Application.persistentDataPath + "/" + path + jsonSufix);
			T component = serializer.Deserialize<T>(new JsonTextReader(streamReader));
			streamReader.Dispose();
			return component;
		}
		else {
			#if UNITY_EDITOR
			TextReader streamReader = new StreamReader(Application.dataPath + "/Resources/" + path + jsonSufix);
			T component = serializer.Deserialize<T>(new JsonTextReader(streamReader));
			streamReader.Dispose();
			return component;
			#elif UNITY_ANDROID
			TextAsset textFile = Resources.Load<TextAsset>(path);
			T component = JsonConvert.DeserializeObject<T>(textFile.text);
			return component;
			#endif
		}
		#endif
	}

	public static void Serialize(object value, string sufix = "") {
		string path = "";
		if (sufix != "") {
			path += "_" + sufix;
		}
		#if UNITY_EDITOR
		path = Application.dataPath + "/Resources/" + value.GetType().Name + path + jsonSufix;
		#elif UNITY_ANDROID
		path = Application.persistentDataPath + "/" + component.GetType().Name + path + xmlSufix;
		#endif

		#if UNITY_WEBPLAYER || UNITY_WEBGL
		PlayerPrefs.SetString(path, JsonConvert.SerializeObject(value));
		PlayerPrefs.Save();
		#else
		JsonSerializer serializer = new JsonSerializer();
		StreamWriter streamWriter = new StreamWriter(path, false);
		serializer.Serialize(streamWriter, value);
		streamWriter.Close();
		#endif
	}
}

