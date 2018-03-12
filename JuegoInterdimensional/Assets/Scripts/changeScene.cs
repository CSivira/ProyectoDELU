using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour {

    public void ChangeToScene(string str)
    {

        SceneManager.LoadScene(str);
        //Debug.Log("CLICK");

    }

    public void QuitScene()
    {

        Application.Quit(); ;

    }
}
