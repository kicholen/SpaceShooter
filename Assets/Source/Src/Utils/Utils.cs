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
		bool exists = File.Exists(Application.persistentDataPath + "/" + path + xmlSufix);
		StreamReader streamReader = new StreamReader(exists 
		                                             ? Application.persistentDataPath + "/" + path + xmlSufix 
		                                             : Application.dataPath + "/Resources/" + path + xmlSufix);
		return serializer.Deserialize(streamReader.BaseStream) as IComponent;
	}

	public static void SerializeComponent(IComponent component) {
		XmlSerializer serializer = new XmlSerializer(component.GetType());
		StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/" + component.GetType().Name + xmlSufix, false);
		serializer.Serialize(streamWriter, component);
		streamWriter.Close();
	}
}

