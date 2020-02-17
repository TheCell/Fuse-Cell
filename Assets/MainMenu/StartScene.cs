using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void StartSceneByID(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
