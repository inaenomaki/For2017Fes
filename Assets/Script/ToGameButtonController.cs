using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGameButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    public void clickedButton()
    {
        GameObject.Find("gameManager").GetComponent<GameManagerController>().init();
        SceneManager.LoadScene("Game");
    }
}
