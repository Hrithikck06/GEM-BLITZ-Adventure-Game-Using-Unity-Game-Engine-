using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
