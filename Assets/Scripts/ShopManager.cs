using UnityEngine;
using static UnityEngine.Rendering.STP;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameDb mDb;

    private Configuracoes config;
    private int idNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        config = mDb.CarregarConfiguracoes();
        idNum = config.Id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPopUp(GameObject popUp)
    {
        popUp.SetActive(true);
    }

    public void ClosePopUp(GameObject popUp)
    {
        popUp.SetActive(false);
    }
    public void ComprarUpgrade(string powerUp)
    {
        mDb.CarregarPowerUps(idNum, powerUp);
        
    }
}
