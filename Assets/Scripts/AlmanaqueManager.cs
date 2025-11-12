using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlmanaqueManager : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private TextMeshProUGUI nomeItemText;
    [SerializeField] private TextMeshProUGUI mensagemInicialText;
    [SerializeField] private Image[] quadrados;

    [Header("Categorias")]
    [SerializeField] private Button categoriaFellas;
    [SerializeField] private Button categoriaFoes;
    [SerializeField] private Button categoriaInventory;

    [Header("Banco de Dados")]
    [SerializeField] private AlmanaqueData almanaqueData;

    private List<AlmanaqueItem> itensCategoriaAtual = new List<AlmanaqueItem>();
    private List<Image> imagensItens = new List<Image>();

    private void Start()
    { 
        categoriaFellas.onClick.AddListener(() => SelecionarCategoria("Fellas"));
        categoriaFoes.onClick.AddListener(() => SelecionarCategoria("Foes"));
        categoriaInventory.onClick.AddListener(() => SelecionarCategoria("Inventory"));

        // comportamento dos quadrados
        for (int i = 0; i < quadrados.Length; i++)
        {
            int index = i;
            Button btn = quadrados[i].GetComponent<Button>();
            if (btn != null)
                btn.onClick.AddListener(() => MostrarDescricaoItem(index));
        }

        // inicial
        nomeItemText.text = "";
        mensagemInicialText.text = "Selecione uma categoria";
    }

    private void SelecionarCategoria(string categoria)
    {
        nomeItemText.text = "";
        mensagemInicialText.text = "Selecione um item";

        // remove as imagens antigas
        foreach (var img in imagensItens)
        {
            if (img != null)
                Destroy(img.gameObject);
        }
        imagensItens.Clear();

        // busca os itens da categoria selecionada
        itensCategoriaAtual = almanaqueData.ObterItensPorCategoria(categoria);

        for (int i = 0; i < quadrados.Length; i++)
        {
            Image quadrado = quadrados[i];

            if (i < itensCategoriaAtual.Count && itensCategoriaAtual[i].sprite != null)
            {
                quadrado.gameObject.SetActive(true);

                // cria uma nova imagem do item como filho do quadrado
                GameObject itemGO = new GameObject("ItemImage", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
                itemGO.transform.SetParent(quadrado.transform, false);

                Image itemImg = itemGO.GetComponent<Image>();
                itemImg.sprite = itensCategoriaAtual[i].sprite;
                itemImg.preserveAspect = true;
                itemImg.type = Image.Type.Simple;
                itemImg.color = new Color(1, 1, 1, 1); // transparente no fundo, mas visível no sprite

                // ajusta posição e tamanho
                RectTransform rect = itemGO.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = Vector2.zero;
                rect.sizeDelta = quadrado.GetComponent<RectTransform>().sizeDelta * 0.7f; // menor que o quadrado

                imagensItens.Add(itemImg);
            }
            else
            {
                quadrado.gameObject.SetActive(false); // esconde quadrado vazio
            }
        }
    }

    private void MostrarDescricaoItem(int index)
    {
        if (index < 0 || index >= itensCategoriaAtual.Count)
            return;

        var item = itensCategoriaAtual[index];
        nomeItemText.text = item.nome;
        mensagemInicialText.text = item.descricao;
    }

    private void LimparQuadrados()
    {
        foreach (var img in quadrados)
        {
            img.sprite = null;
            img.color = Color.white; // mantém a cor original
            img.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}
