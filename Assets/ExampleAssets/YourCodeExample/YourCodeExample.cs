using UnityEngine;
using WindowManager;

public class YourCodeExample : MonoBehaviour
{
    private void Start()
    {
        Window.Init(false);
        Window.Open("Info Panel");
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
