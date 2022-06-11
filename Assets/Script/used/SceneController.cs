using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private string[] loadedScenes;
    void Start()
    {
        int countLoaded = SceneManager.sceneCountInBuildSettings;
        loadedScenes = new string[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)); ;
            Debug.Log(loadedScenes[i]);
        }
    }

    public void OnLoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(loadedScenes[sceneNumber]);
    }
}
