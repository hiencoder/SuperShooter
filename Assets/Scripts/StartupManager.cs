using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartupManager : MonoBehaviour {

	// Use this for initialization
	public IEnumerator Start () {
		while(!LocalizationManager.instance.GetIsReady()){
			yield return null;
		}
		SceneManager.LoadScene("MenuScene",LoadSceneMode.Single);
	}
	
}
