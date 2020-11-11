using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{ 
    public void LoadGameScene(SceneList scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
