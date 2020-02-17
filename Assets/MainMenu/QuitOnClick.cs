using UnityEngine;

public class QuitOnClick : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
