using System.Xml;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System;
using Entitas;

public static class Utils
{
	const string xmlSufix = ".xml";

	public static XmlNode LoadXml(string path) {
		XmlDocument document = new XmlDocument();
		TextAsset textFile = Resources.Load<TextAsset>(path);
		document.LoadXml(textFile.text);
		return document.FirstChild;
	}

	public static IComponent DeserializeComponent(Type type) {
		string path = type.Name;
		XmlSerializer serializer = new XmlSerializer(type);
		if (File.Exists(Application.persistentDataPath + "/" + path + xmlSufix)) {
			StreamReader streamReader = new StreamReader(Application.persistentDataPath + "/" + path + xmlSufix);
			IComponent component = serializer.Deserialize(streamReader.BaseStream) as IComponent;
			streamReader.Dispose();
			return component;
		}
		else {
			#if UNITY_EDITOR
				StreamReader streamReader = new StreamReader(Application.dataPath + "/Resources/" + path + xmlSufix);
				IComponent component = serializer.Deserialize(streamReader.BaseStream) as IComponent;
				streamReader.Dispose();
				return component;
			#elif UNITY_ANDROID
				TextAsset textFile = Resources.Load<TextAsset>(path);
				StringReader stringReader = new StringReader(textFile.text);
				XmlReader xmlReader = XmlReader.Create(stringReader);
				IComponent component = serializer.Deserialize(xmlReader) as IComponent;
				stringReader.Dispose();
				xmlReader.Close();
				return component;
			#endif
		}
	}

	public static void SerializeComponent(IComponent component) {
		XmlSerializer serializer = new XmlSerializer(component.GetType());
		StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/" + component.GetType().Name + xmlSufix, false);
		serializer.Serialize(streamWriter, component);
		streamWriter.Close();
	}
}

