		#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;



namespace Utility
{
	public static class EditorToolsUtility
	{
		/// <summary>
		/// Creates an asset with name assetName.asset in the folder specified in folderPath.
		/// </summary>
		/// <param name="folderPath">For example "Assets/Scripts/".</param>
		/// <param name="assetName">Asset name without ".asset". If an asset with this name already exists the name will be appended with an index according to your editor settings.</param>
		/// <typeparam name="T">Subclass of ScriptableObject. If not a subclass, simply enter "ScriptableObject" as T.</typeparam>
		/// <returns></returns>
		public static T CreateScriptableObjectAsset<T>(string folderPath, out string completeAssetPathAndName, string assetName = "") where T : ScriptableObject
		{
			// set default name
			if (assetName == "")
			{
				assetName = typeof(T).ToString();
			}
			
			// create asset
			T newAsset = ScriptableObject.CreateInstance<T>();
			string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (folderPath + assetName + ".asset");
			completeAssetPathAndName = assetPathAndName;
			
			AssetDatabase.CreateAsset(newAsset, assetPathAndName);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			return newAsset;
		}
		//
		// public static void CreateFolder(string parentFolderPath, string folderName, string out completeAssetPathAndName)
		// {
		// 	
		// 	AssetDatabase.FindAssets()
		// 	AssetDatabase.CreateFolder();
		// }

		public static string GetAssetName(string completeAssetPathWithName)
		{
			int lastSlash = completeAssetPathWithName.LastIndexOf("/");
			int lastDot = completeAssetPathWithName.LastIndexOf(".");
			int length = lastSlash - lastDot;
			string assetName = completeAssetPathWithName.Substring(lastSlash + 1, length);
			
			return assetName;
		}

		public static bool DoesAssetWithNameExist(string assetName, string[] searchFolders)
		{
			var foundAssets = AssetDatabase.FindAssets(assetName, searchFolders);
			return foundAssets.Length != 0;
		}

		public static bool DeleteScriptableObjectAsset<T>(T asset) where T : ScriptableObject
		{
			return AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset));
		}
		
		
		
		/// <summary>
		/// For EditorGUILayout titles in a custom editor.
		/// </summary>
		/// <param name="text"></param>
		public static void TitleAuto(string text)
		{
			GUIStyle style = new GUIStyle {fontSize = 14};
			string titleText = "<color=(0, 0, 0>" + text + "</color>";
			EditorGUILayout.LabelField(titleText, style);
		}

		/// <summary>
		/// For EditorGUILayout titles in a custom editor.
		/// </summary>
		public static void DetailsAuto(string inputText)
		{
			EditorGUILayout.BeginHorizontal();
			GUIStyle style = new GUIStyle(EditorStyles.helpBox) {wordWrap = true};
			GUILayout.Label(inputText, style);
			EditorGUILayout.EndHorizontal();
		}
		
		/// <summary>
		/// ObjectField with EditorGUILayout.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static void ObjectFieldAuto<T>(ref T obj, bool allowSceneObjects = true) where T : UnityEngine.Object
		{
			obj = (T) EditorGUILayout.ObjectField(obj, typeof(T), allowSceneObjects);
		}

		public static void ObjectFieldAuto<T>(ref T obj, string details, bool allowSceneObjects = true) where T : UnityEngine.Object
		{
			obj = (T) EditorGUILayout.ObjectField(obj, typeof(T), allowSceneObjects);
			DetailsAuto(details);
		}
		
		/// <summary>
		/// ObjectField with EditorGUI.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static void ObjectField<T>(ref T obj, Rect positionAndSize, bool allowSceneObjects = true) where T : UnityEngine.Object
		{
			obj = (T) EditorGUI.ObjectField(positionAndSize, obj, typeof(T), allowSceneObjects);
		}
		
	}
}
#endif
