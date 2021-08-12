using UnityEditor;
using UnityEngine;
using Utility;

namespace Dialogue
{
	[CustomEditor(typeof(Dialogue))]
	public class DialogueEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			Dialogue thiss = (Dialogue) target;
			
			if (GUILayout.Button("Add container"))
			{
				thiss.startContainer = EditorToolsUtility.CreateScriptableObjectAsset<DialogueContainer>("Assets/Scripts/Dialogue/Containers/",
					out string completePath, "New Dialogue Container");
			}
		}
	}

}
