using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yuuta.Halloween
{
    public class Button : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}