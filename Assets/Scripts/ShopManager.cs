using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.STP;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameDb mDb;
    [SerializeField] private Texture[] arrBarsImage;
    [SerializeField] private TextMeshProUGUI[] arrText;
    [SerializeField] private RawImage[] arrBars;
    [SerializeField] private string[] arrBarsName;
    [SerializeField] private TextMeshProUGUI totalCoins;

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

        for(int i = 1; i < 5; i++)
        {
            string powerUpName = arrBarsName[i - 1];
            RawImage objetoBar = arrBars[i - 1];
            TextMeshProUGUI strPrice = arrText[i - 1];
            PowerUpsTable upgradeTable = mDb.CarregarPowerUps(idNum, powerUpName);
            int IndexLevel = upgradeTable.Nivel;
            strPrice.text = "" + upgradeTable.Price;
            objetoBar.texture = arrBarsImage[IndexLevel];
        }
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

    public void ComprarUpgrade(GameObject powerUp)//, TextMeshProUGUI priceUI, RawImage barra)
    {
        TextMeshProUGUI priceUI = powerUp.transform.Find("BuyUpgrade/PriceButton").GetComponent<TextMeshProUGUI>() ;
        RawImage barra = powerUp.transform.Find("BarraCompra").GetComponent<RawImage>();
        string powerUpName = powerUp.name;

        coins = progress.Coins;
        PowerUpsTable upgradeTable = mDb.CarregarPowerUps(idNum, powerUpName);
        price = upgradeTable.Price;

        Debug.Log(price);
        Debug.Log(coins);

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

        if (priceUI == null || barra == null)
        {
            Debug.LogError($"PowerUp '{powerUp}' n o reconhecido.");
            return;
        }
        coins = coins - price;
        totalCoins.text = "" + coins;
        priceUI.text = "" + newPrice;
        barra.texture = arrBarsImage[newLevel];

        mDb.SalvarPowerUps(idNum, powerUpName, newLevel, newDuration, newPrice);

    }
}
