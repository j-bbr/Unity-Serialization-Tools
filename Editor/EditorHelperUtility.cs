using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EditorHelper{
	public static class EditorHelperUtility{
		/// <summary>
		/// returns the asset folder path given an absolute path
		/// </summary>
		/// <returns>The unity path.</returns>
		/// <param name="absolutePath">Absolute path.</param>
		public static string getUnityPath(string absolutePath)
		{
			string[] folders = absolutePath.Split(Path.DirectorySeparatorChar);
			for(int i = 0; i< folders.Length; i++)
			{	
				if(folders[i].Contains("Assets"))
				{
					int length = folders.Length-i;
					string[] unityFolders = new string[length];
					Array.Copy(folders, i, unityFolders,0, length);
					//all unity paths use forward slashes
					absolutePath = string.Join("/", unityFolders);
					break;
				}
			}
			return absolutePath;
		}
		/// <summary>
		/// getting the selected folder
		/// </summary>
		/// <returns>The selected path.</returns>
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
		/// <summary>
		/// Gets the folder objects of type T with the given extension
		/// </summary>
		/// <returns>The folder objects.</returns>
		/// <param name="path">Path.</param>
		/// <param name="extension">Extension.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static List<T> getFolderObjects<T>(string path, string extension)
		{
			string[] files = Directory.GetFiles(path);
			//Order is not guaranteed with Directory.GetFiles
			Array.Sort(files);
			List<T> objects = new List<T>();
			foreach(string file in files)
			{
				if(!file.EndsWith("."+ extension)) continue;
				T assetObject = (T)Convert.ChangeType(AssetDatabase.LoadAssetAtPath(getUnityPath(file), typeof(T)), typeof(T));
				objects.Add(assetObject);
			}
			return objects;
		}
		/// <summary>
		/// Gets a type from a path assuming the filename is identical to the classname
		/// and the extension is .cs
		/// </summary>
		/// <returns>The from path.</returns>
		/// <param name="path">Path.</param>
		public static Type getScriptTypeFromPath(string path)
		{
			string[] folders = path.Split(Path.DirectorySeparatorChar);
			string classname = folders[folders.Length-1].Remove(folders[folders.Length-1].Length-3);
			return getTypeFromName(classname);
		}
		public static Type getTypeFromName(string classname)
		{
			System.Reflection.Assembly[] AS = System.AppDomain.CurrentDomain.GetAssemblies();
			
			foreach (var A in AS)
			{
				Type[] types = A.GetTypes();
				foreach (Type t in types)
				{
					if (t.Name == classname) return t;
				}
			}
			return null;
		}
		public static void saveToPath(string contents, string path)
		{
			if(!string.IsNullOrEmpty(path))
			{
				File.WriteAllText(path, contents);
				//Let Unity know that there is a new asset
				AssetDatabase.Refresh();
			}
		}
	}
}
