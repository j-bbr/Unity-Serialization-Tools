#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System;
namespace CustomSerialization.Templates{ 
	public class ElementHolder : ScriptableObject {

		public virtual void SaveElementToXML()
		{

		}
		public virtual void SaveElementToJSON()
		{
			
		}
	}
}
#endif
