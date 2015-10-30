using System.Xml;
using UnityEngine;

public static class Utils
{
	public static XmlNode loadXml(string path)
	{
		XmlDocument doc = new XmlDocument();
		TextAsset textFile = Resources.Load<TextAsset>(path);
		doc.LoadXml(textFile.text);
		return doc.FirstChild;
	}
}

