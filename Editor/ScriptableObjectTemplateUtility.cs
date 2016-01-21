using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Text;
using EditorHelper;

namespace XMLSerialization
{
	public static class ScriptableObjectTemplateUtility
	{	
		public const string TemplateSubMenuName = "Templates";

		[MenuItem ("Window/Create Scriptable Object Template")]
		public static void getScriptInfo()
		{
			string path = EditorUtility.OpenFilePanel("Choose Script", EditorHelperUtility.getSelectedPath(), "cs");
			if(string.IsNullOrEmpty(path)) return;
			Type type = EditorHelperUtility.getScriptTypeFromPath(path);
			if(type == null)
			{
				Debug.Log("There has been some error getting the script data");
				return;
			}else 
			{
				createTemplate(type.Name);
			}

		}
		private static void createTemplate(string classname)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine("#if UNITY_EDITOR");
			builder.AppendLine("// Auto Generated Serialization Template").AppendLine("namespace CustomSerialization.Templates{ ");
			builder.AppendLine("using CustomSerialization;").AppendLine("using UnityEngine;");
			builder.AppendLine("[CreateAssetMenu(fileName=\"" +classname +"\", menuName = \"" + TemplateSubMenuName +"/"+classname+"\", order = 1000)]");
			builder.AppendLine("public class "+classname+ "Holder : ElementHolder{");
		
			builder.AppendLine("").AppendLine("public "+classname+" "+ classname+"Element;");
			builder.AppendLine("public override void SaveElementToXML()").AppendLine("{");
			builder.AppendLine("XMLSaver.saveToFile("+classname+"Element, typeof("+classname+"));").AppendLine("}");

			builder.AppendLine("public override void SaveElementToJSON()").AppendLine("{");
			builder.AppendLine("JSONSaver.saveToFile("+classname+"Element, typeof("+classname+"));").AppendLine("}");

			builder.AppendLine("}").AppendLine("}").AppendLine("#endif");
			string path = EditorUtility.SaveFilePanel("Save the Templatefile", EditorHelperUtility.getSelectedPath(), classname+"Holder", "cs");
			EditorHelperUtility.saveToPath(builder.ToString(), path);
		}
	}
}
