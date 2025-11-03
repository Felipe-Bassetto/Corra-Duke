using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlmanaqueManager : MonoBehaviour
{
    [Header("Referências de UI")]
    public TextMeshProUGUI centralText;
    public List<Button> losangos = new List<Button>();

    [Header("Botões de Categorias")]
    public Button fellasButton;
    public Button foesButton;
    public Button inventoryButton;

    [Header("Sprites")]
    public Sprite defaultSprite;

    [Header("Fonte de Dados")]
    public AlmanaqueData dataSource; // Referência p/ outro script

    private Dictionary<string, List<ItemInfo>> categorias = new Dictionary<string, List<ItemInfo>>();
    private string categoriaAtual = "";

    void Start()
    {
        centralText.text = "Selecione uma categoria";

        // Carrega os dados do AlmanaqueData 
        categorias = dataSource.GetCategorias();

        // Clique das categorias
        fellasButton.onClick.AddListener(() => MostrarCategoria("Fellas"));
        foesButton.onClick.AddListener(() => MostrarCategoria("Foes"));
        inventoryButton.onClick.AddListener(() => MostrarCategoria("Inventory"));

        //  Cliques dos losangos
        for (int i = 0; i < losangos.Count; i++)
        {
            int index = i;
            losangos[i].onClick.AddListener(() => MostrarDescricao(index));
            losangos[i].image.sprite = defaultSprite;
        }
    }

    // Exibe os itens da categoria escolhida
    void MostrarCategoria(string categoria)
    {
        categoriaAtual = categoria;
        centralText.text = "Selecione um item da categoria " + categoria;

        if (!categorias.ContainsKey(categoria)) return;

        var lista = categorias[categoria];

        for (int i = 0; i < losangos.Count; i++)
        {
            if (i < lista.Count)
            {
                //  Sprite do item 
                losangos[i].image.sprite = lista[i].sprite ?? defaultSprite;
                losangos[i].interactable = true;
            }
            else
            {
                losangos[i].image.sprite = defaultSprite;
                losangos[i].interactable = false;
            }
        }
    }

    //  Descrição do item clicado no centro
    void MostrarDescricao(int index)
    {
        if (string.IsNullOrEmpty(categoriaAtual)) return;
        if (!categorias.ContainsKey(categoriaAtual)) return;

        var lista = categorias[categoriaAtual];
        if (index < 0 || index >= lista.Count) return;

        var item = lista[index];
        centralText.text = item.nome + "\n\n" + item.descricao;
    }
}
