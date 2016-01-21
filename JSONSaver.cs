#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections;


namespace CustomSerialization
{
	public static class JSONSaver
	{
		public static bool PrettyPrint = true;

		public static void saveToFile(object obj, Type type )
		{
			#if !UNITY_5_2 && !UNITY_5_1 && !UNITY_5_0
			if(obj == null) return;
			string filepath = EditorUtility.SaveFilePanel("Save the XML file", XMLSaver.getSelectedPath(), type.Name, "json");	
			string contents = JsonUtility.ToJson(obj, PrettyPrint);
			if(!string.IsNullOrEmpty(filepath))
			{
				File.WriteAllText(filepath, contents);
				//Let Unity know that there is a new asset
				AssetDatabase.Refresh();
			}
			#endif
		}
	}
}
#endif
