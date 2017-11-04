using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToTitleButtonController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void clickedButton()
    {

        Destroy(GameObject.Find("gameManager"));
        SceneManager.LoadScene("Title");
    }
}
