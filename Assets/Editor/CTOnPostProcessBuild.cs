using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Xml;
#endif

public class CTOnPostProcessBuild : Editor {
	#if UNITY_IOS || UNITY_EDITOR  

	[PostProcessBuild (100)]
	public static void OnPostprocessBuild(BuildTarget BuildTarget, string path)  
	{  
		if (BuildTarget == BuildTarget.iOS)  
		{  
			string projPath = PBXProject.GetPBXProjectPath(path);  
			PBXProject proj = new PBXProject();  
			proj.ReadFromString(File.ReadAllText(projPath));  
			 
			//add Other link flag
			string target = proj.TargetGuidByName(PBXProject.GetUnityTargetName());  
			proj.AddBuildProperty (target, "OTHER_LDFLAGS", "-ObjC");
			File.WriteAllText(projPath, proj.WriteToString()); 

			//add framework
			proj.AddFrameworkToProject (target, "AdSupport.framework", false);  
			proj.AddFrameworkToProject (target, "StoreKit.framework", false);  
			proj.AddFrameworkToProject (target, "AVFoundation.framework", false);  
			proj.AddFrameworkToProject (target, "SystemConfiguration.framework", false);  
			proj.AddFrameworkToProject (target, "JavaScriptCore.framework", false);  
			proj.AddFrameworkToProject (target, "ImageIO.framework", false);  
			proj.AddFrameworkToProject (target, "UIKit.framework", false);  
			File.WriteAllText(projPath, proj.WriteToString()); 

			//add ATS in plist
			string plistPath = path + "/Info.plist";  
			PlistDocument plist = new PlistDocument();  
			plist.ReadFromString(File.ReadAllText(plistPath));  
			PlistElementDict dictTransportSecurity = plist.root ["NSAppTransportSecurity"].AsDict ();
			dictTransportSecurity.SetBoolean("NSAllowsArbitraryLoads",true);
			File.WriteAllText(plistPath, plist.WriteToString());
		}  
	}  

	#endif
}
