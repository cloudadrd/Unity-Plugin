using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTChangeScene : MonoBehaviour {
	public Button changeSceneBtn;
	// Use this for initialization
	void Start () {
		changeSceneBtn.onClick.AddListener (changeSceneClick);
	}
	
	void changeSceneClick(){
		Application.LoadLevel ("CTServiceU3DTestCase");
	}
}
