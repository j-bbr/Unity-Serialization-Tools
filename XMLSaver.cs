#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;

namespace CustomSerialization
{
	public static class XMLSaver{

		public static void saveToFile(object obj, Type type )
		{
			var serializer = new XmlSerializer(type);
			if(obj == null) return;
			string filepath = EditorUtility.SaveFilePanel("Save the XML file", getSelectedPath(), type.Name, "xml");	
			string contents;
			using(StringWriter textWriter = new StringWriter())
			{
				serializer.Serialize(textWriter, obj);
				contents = textWriter.ToString();
			}
			if(!string.IsNullOrEmpty(filepath))
			{
				File.WriteAllText(filepath, contents);
				//Let Unity know that there is a new asset
				AssetDatabase.Refresh();
			}
		}
		public static string getSelectedPath()
		{
			string path;
			foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
			{
				path = AssetDatabase.GetAssetPath(obj);
				if ( !string.IsNullOrEmpty(path) && File.Exists(path) ) 
				{
					return Path.GetDirectoryName(path);
				}
			}
			//fallback
			return "Assets";
		}
	}
}
#endif
