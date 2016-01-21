using UnityEngine;
using System;
using System.Collections;


namespace CustomSerialization{
	public static class ImageSaver{
		public static string SaveImageToString(Texture2D image)
		{
			if(image == null ) return null;
			byte[] imagebytes = image.EncodeToPNG();
			return Convert.ToBase64String (imagebytes);
		}
		public static void SaveImageToPrefs(string PrefKey, Texture2D image)
		{
			PlayerPrefs.SetString(PrefKey, SaveImageToString(image));
		}
		public static Texture2D LoadImageFromString(string baseEncodedString)
		{
			Texture2D tex = new Texture2D(2,2);
			if(!string.IsNullOrEmpty(baseEncodedString) && tex.LoadImage(Convert.FromBase64String(baseEncodedString)))
				return tex;
			else return null;
		}
		public static Texture2D LoadImageFromPrefs(string PrefKey)
		{
			return LoadImageFromString(PlayerPrefs.GetString(PrefKey));
		}
	}
}
