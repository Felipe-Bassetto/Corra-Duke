using UnityEngine;
using static UnityEngine.Rendering.STP;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameDb mDb;

    private Configuracoes config;
    private int idNum;
    private Progresso progress;
    private int newPrice;
    private int newLevel;
    private int newDuration;
    private int coins;
    private int price;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        config = mDb.CarregarConfiguracoes();
        
        idNum = config.Id;

        progress = mDb.CarregarProgresso(idNum);
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
        coins = progress.Coins;
        PowerUpsTable upgradeTable = mDb.CarregarPowerUps(idNum, powerUp);
        price = upgradeTable.Price;

        if(coins<price)
        {
            Debug.Log("Sem moedas suficiente");
            return;
        }

        newLevel = upgradeTable.Nivel + 1;

        switch(newLevel)
        {
            case 1:
                newPrice = 1000;
                newDuration = 5;
                break;
            case 2:
                newPrice = 2500;
                newDuration = 8;
                break;
            case 3:
                newPrice = 4000;
                newDuration = 11;
                break;
            case 4:
                newPrice = 5000;
                newDuration = 15;
                break;
            case 5:
                newPrice = 7500;
                newDuration = 18;
                break;
            case 6:
                newPrice = 10000;
                newDuration = 22;
                break;
            case 7:
                newPrice = 0;
                newDuration = 25;
                break;
        }

        mDb.SalvarPowerUps(idNum, powerUp, newLevel, newDuration, newPrice);
        
    }
}
