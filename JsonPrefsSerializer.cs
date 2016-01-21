using UnityEngine;
using System.Collections;

namespace CustomSerialization
{
	public static class JsonPrefsSerializer{
		#if !UNITY_5_2 && !UNITY_5_1 && !UNITY_5_0
		public static void SaveJson(string prefKey, object serializableObject)
		{
			string jsonString = SaveToJsonString(serializableObject);
			if(jsonString == null) return;
			PlayerPrefs.SetString (prefKey, jsonString);
		}
		public static T RetrieveJson<T> (string prefKey)
		{
			return RetrieveFromJsonString<T>(PlayerPrefs.GetString (prefKey, string.Empty));
		}
		
		public static string SaveToJsonString(object serializableObject)
		{
			if(serializableObject == null) return null;
			return JsonUtility.ToJson(serializableObject);
		}
		public static T RetrieveFromJsonString<T> (string content)
		{
			if (string.IsNullOrEmpty(content)) return default(T);
			return JsonUtility.FromJson<T>(content);
		}
		public static T RetrieveJsonFromRessources<T> (string ressourcespath)
		{
			if (string.IsNullOrEmpty(ressourcespath) )
				return default(T);
			TextAsset jsondata = Resources.Load(ressourcespath) as TextAsset;
			if(jsondata == null) return default(T);
			return RetrieveFromJsonString<T>(jsondata.text);
		}
		#endif
	}
}
