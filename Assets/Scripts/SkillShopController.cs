using UnityEngine;

public class SkillShopController : MonoBehaviour
{
    [SerializeField] private KeyCode exitKey = KeyCode.Return;
    private bool shopExited = false;

    void Update()
    {
        if (!shopExited && Input.GetKeyDown(exitKey))
        {
            shopExited = true;
            GameManager.Instance.OnExitShop();
        }
    }
}