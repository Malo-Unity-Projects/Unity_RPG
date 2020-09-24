using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToShop : MonoBehaviour
{
    public void OnClick()
    {
        GlobalController.Instance.state = GlobalController.GameState.SHOP;
        GlobalController.Instance.hasLoaded = false;
        SceneManager.LoadScene("ShopScene", LoadSceneMode.Single);
    }
}
