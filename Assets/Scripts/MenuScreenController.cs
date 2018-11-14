using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScreenController : MonoBehaviour {
	public void StartGame(){
		SceneManager.LoadScene("Game",LoadSceneMode.Single);
	}

	public void ExitGame(){
		if(Application.isPlaying){
			Application.Quit();
		}
	}
}
