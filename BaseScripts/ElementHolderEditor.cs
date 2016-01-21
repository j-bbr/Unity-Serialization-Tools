#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using CustomSerialization.Templates;

[CustomEditor(typeof(ElementHolder), true)]
public class ElementHolderEditor : Editor
{
	
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		ElementHolder script = (ElementHolder)target;
		if(GUILayout.Button("Save to XML Asset"))
		{
			script.SaveElementToXML();
		}
		if(GUILayout.Button("Save to JSON Asset"))
		{
			script.SaveElementToJSON();
		}
	}
}
#endif