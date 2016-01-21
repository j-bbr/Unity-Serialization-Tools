using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.IO;

namespace CustomSerialization{
	public static class XMLPrefsSerializer{

		public static void SaveXML<T>(string prefKey, T serializableObject)
		{
			string xmlString = SaveToXMLString<T>(serializableObject);
			if(xmlString == null) return;
			PlayerPrefs.SetString (prefKey, xmlString);
		}
		public static T RetrieveXML<T> (string prefKey)
		{
			string tmp = PlayerPrefs.GetString (prefKey, string.Empty);
			return RetrieveFromXMLString<T>(tmp);
		}

		public static string SaveToXMLString<T>(T serializableObject)
		{
			var serializer = new XmlSerializer(typeof(T));
			if(serializableObject == null) return null;
			using(StringWriter textWriter = new StringWriter())
			{
				serializer.Serialize(textWriter, serializableObject);
				return textWriter.ToString();
			}
		}
		public static T RetrieveFromXMLString<T> (string content)
		{
			if (string.IsNullOrEmpty(content) )
				return default(T);
			var serializer = new XmlSerializer(typeof(T));
			using(TextReader textReader = new StringReader(content))
			{
				return (T)Convert.ChangeType(serializer.Deserialize(textReader), typeof(T));
			}
		}
		public static T RetrieveXMLFromRessources<T> (string ressourcespath)
		{
			if (string.IsNullOrEmpty(ressourcespath) )
				return default(T);
			TextAsset xmldata = Resources.Load(ressourcespath) as TextAsset;
			if(xmldata == null) return default(T);
			return RetrieveFromXMLString<T>(xmldata.text);
		}
	}
}
