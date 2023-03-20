// AssetBundleTool is designed for general Unity environment.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine.SceneManagement;
#if UNITY_2018_1_OR_NEWER
using UnityEditor.Build.Reporting;
#endif

namespace AssetBundleTool
{
	public class AssetBundleObjectLoaderBuildProcess :
#if UNITY_2018_1_OR_NEWER
		IProcessSceneWithReport
#else
		IProcessScene
		
#endif
	{
		public int callbackOrder { get { return 0; } }

#if UNITY_2018_1_OR_NEWER
		public void OnProcessScene(Scene scene, BuildReport report)
		{
			OnProcessScene(scene);
		}
#endif

		List<string> aboPath = new List<string>();

		public void FindAssetBundleObjectLoader(GameObject obj)
		{
			var abos = obj.GetComponentsInChildren<AssetBundleObjectLoader>();
			foreach (var abo in abos)
			{
				aboPath.Add(abo.assetTypeName);
			}
		}

		public void OnProcessScene(Scene scene)
		{
			GameObject [] rootObjs = scene.GetRootGameObjects();
			foreach (var obj in rootObjs)
			{
				FindAssetBundleObjectLoader(obj);
			}
		}
	}
}
