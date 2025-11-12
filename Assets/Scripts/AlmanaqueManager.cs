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

    private void Start()
    {
        // liga com as categorias 
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

        // estado inicial
        nomeItemText.text = "";
        mensagemInicialText.text = "Selecione uma categoria";
    }

    private void SelecionarCategoria(string categoria)
    {
        nomeItemText.text = "";
        mensagemInicialText.text = "Selecione um item";

        // busca os itens da categoria selecionada
        itensCategoriaAtual = almanaqueData.ObterItensPorCategoria(categoria);

        for (int i = 0; i < quadrados.Length; i++)
        {
            Image img = quadrados[i];

            if (i < itensCategoriaAtual.Count && itensCategoriaAtual[i].sprite != null)
            {
                img.gameObject.SetActive(true);
                img.sprite = itensCategoriaAtual[i].sprite;
                img.color = Color.white;

                // faz o sprite aparecer dentro do quadrado, sobrepondo
                img.preserveAspect = true;
                img.type = Image.Type.Simple;

                RectTransform rect = img.GetComponent<RectTransform>();
                rect.localScale = Vector3.one;
                rect.SetAsLastSibling(); // garante que o item fique sobre o quadrado
            }
            else
            {
                img.gameObject.SetActive(false); // esconde quadrado vazio
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
            img.color = new Color(1, 1, 1, 0.25f);
            img.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}
