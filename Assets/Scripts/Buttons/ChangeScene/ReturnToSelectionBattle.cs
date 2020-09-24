using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToSelectionBattle : MonoBehaviour
{
    public void OnClick()
    {
        GlobalController.Instance.state = GlobalController.GameState.RESTART;
        GlobalController.Instance.hasLoaded = false;
        GlobalController.Instance.xp = 0;
        GlobalController.Instance.money = 0;
        for (int i = GlobalController.Instance.enemyMutants.Count; i != 0; i--) {
            Mutant m = GlobalController.Instance.enemyMutants[0];
            GlobalController.Instance.enemyMutants.Remove(GlobalController.Instance.enemyMutants[0]);
            Destroy(m.gameObject);
        }
        SceneManager.LoadScene("SelectionBattle", LoadSceneMode.Single);
    }
}
