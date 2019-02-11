using UnityEngine;

namespace WindowManager {
    public class ManageHelper : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Window.CloseLast();
        }

        public void Open(string name)
        {
            Window.Open(name);
        }
        public void Close(string name)
        {
            Window.Close(name);
        }
    }
}
