using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMissionButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("SelectionBattle", LoadSceneMode.Single);
    }
}
