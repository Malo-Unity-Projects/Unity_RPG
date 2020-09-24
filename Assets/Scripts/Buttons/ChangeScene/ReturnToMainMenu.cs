using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public void OnClick()
    {
        GlobalController.Instance.state = GlobalController.GameState.RESTART;
        GlobalController.Instance.hasLoaded = false;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
