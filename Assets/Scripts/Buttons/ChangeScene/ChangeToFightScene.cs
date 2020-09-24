using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToFightScene : MonoBehaviour
{
    public void OnClick()
    {
        GlobalController.Instance.state = GlobalController.GameState.FIGHT;
        GlobalController.Instance.hasLoaded = false;
        SceneManager.LoadScene("FightScene", LoadSceneMode.Single);
    }
}
