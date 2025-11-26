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

    [Header("Imagem Grande do Item")]
    [SerializeField] private Image imagemGrande;

    [Header("Categorias")]
    [SerializeField] private Button categoriaFellas;
    [SerializeField] private Button categoriaFoes;
    [SerializeField] private Button categoriaInventory;

    [Header("Banco de Dados")]
    [SerializeField] private AlmanaqueData almanaqueData;

    [Header("Frases Aleatórias")]
    [SerializeField] private string[] frasesAleatorias;

    private List<AlmanaqueItem> itensCategoriaAtual = new List<AlmanaqueItem>();
    private List<Image> imagensItens = new List<Image>();

    private void Start()
    { 
        categoriaFellas.onClick.AddListener(() => SelecionarCategoria("Fellas"));
        categoriaFoes.onClick.AddListener(() => SelecionarCategoria("Foes"));
        categoriaInventory.onClick.AddListener(() => SelecionarCategoria("Inventory"));

        // Esconde todos os quadrados ao abrir 
        foreach (var quad in quadrados)
            quad.gameObject.SetActive(false);

        for (int i = 0; i < quadrados.Length; i++)
        {
            int index = i;
            Button btn = quadrados[i].GetComponent<Button>();
            if (btn != null)
                btn.onClick.AddListener(() => MostrarDescricaoItem(index));
        }

        nomeItemText.text = "";

        if (frasesAleatorias != null && frasesAleatorias.Length > 0)
            mensagemInicialText.text = frasesAleatorias[Random.Range(0, frasesAleatorias.Length)];
        else
            mensagemInicialText.text = "Selecione uma categoria";

        if (imagemGrande != null)
            imagemGrande.gameObject.SetActive(false);
    }

    private void SelecionarCategoria(string categoria)
    {
        nomeItemText.text = "";
        mensagemInicialText.text = "O que deseja consultar hoje ?";

        if (imagemGrande != null)
        {
            imagemGrande.sprite = null;
            imagemGrande.gameObject.SetActive(false);
        }

        // remove imagens antigas 
        foreach (var img in imagensItens)
        {
            if (img != null)
                Destroy(img.gameObject);
        }
        imagensItens.Clear();

        itensCategoriaAtual = almanaqueData.ObterItensPorCategoria(categoria);

        for (int i = 0; i < quadrados.Length; i++)
        {
            Image quadrado = quadrados[i];

            // usa spriteIcon 
            if (i < itensCategoriaAtual.Count && itensCategoriaAtual[i].spriteIcon != null)
            {
                quadrado.gameObject.SetActive(true);

                GameObject itemGO = new GameObject("ItemImage", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
                itemGO.transform.SetParent(quadrado.transform, false);

                Image itemImg = itemGO.GetComponent<Image>();
                itemImg.sprite = itensCategoriaAtual[i].spriteIcon; //  Icon
                itemImg.preserveAspect = true;
                itemImg.type = Image.Type.Simple;

                RectTransform rect = itemGO.GetComponent<RectTransform>();
                rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = Vector2.zero;
                rect.sizeDelta = quadrado.GetComponent<RectTransform>().sizeDelta * 0.7f;

                imagensItens.Add(itemImg);
            }
            else
            {
                quadrado.gameObject.SetActive(false);
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

        if (imagemGrande != null)
        {
            imagemGrande.sprite = item.spriteGrande; 
            imagemGrande.preserveAspect = true;
            imagemGrande.gameObject.SetActive(true);
        }
    }

    private void LimparQuadrados()
    {
        foreach (var img in quadrados)
        {
            img.sprite = null;
            img.color = Color.white;
            img.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}
