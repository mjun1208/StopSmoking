using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public static ChangeScene instance;

    private void Awake()
    {
        instance = this;    
    }

    public void Change_Scene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Scene_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Start is called before the first frame update

    // Update is called once per frame
}
