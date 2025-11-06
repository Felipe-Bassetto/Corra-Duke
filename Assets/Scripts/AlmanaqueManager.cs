using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlmanaqueManager : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private TextMeshProUGUI nomeItemText;           
    [SerializeField] private TextMeshProUGUI mensagemInicialText;    
    [SerializeField] private Image[] losangos;                       

    [Header("Categorias")]
    [SerializeField] private Button categoriaFellas;
    [SerializeField] private Button categoriaFoes;
    [SerializeField] private Button categoriaInventory;

    [Header("Banco de Dados")]
    [SerializeField] private AlmanaqueData almanaqueData;

    private List<AlmanaqueItem> itensCategoriaAtual = new List<AlmanaqueItem>();

    private void Start()
    {
        // ligações com as categorias corretas
        categoriaFellas.onClick.AddListener(() => SelecionarCategoria("Fellas"));
        categoriaFoes.onClick.AddListener(() => SelecionarCategoria("Foes"));
        categoriaInventory.onClick.AddListener(() => SelecionarCategoria("Inventory"));

        // define o comportamento dos losangos
        for (int i = 0; i < losangos.Length; i++)
        {
            int index = i;
            Button btn = losangos[i].GetComponent<Button>();
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
        mensagemInicialText.text = "";

        // busca os itens da categoria selecionada
        itensCategoriaAtual = almanaqueData.ObterItensPorCategoria(categoria);
        mensagemInicialText.text = "Selecione um item";


        for (int i = 0; i < losangos.Length; i++)
        {
            Image img = losangos[i];
            
            if (i < itensCategoriaAtual.Count && itensCategoriaAtual[i].sprite != null)
              {
                 img.gameObject.SetActive(true);
                 img.sprite = itensCategoriaAtual[i].sprite;
                 img.color = Color.white;

                 // ajusta a imagem dentro do losango
                 img.preserveAspect = true;
                 img.type = Image.Type.Simple;

                 RectTransform rect = img.GetComponent<RectTransform>();
                  rect.localScale = new Vector3(0.8f, 0.8f, 1f);
              }
            else
              {
                img.gameObject.SetActive(false); // esconde  o losango sem item
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

    private void LimparLosangos()
    {
        foreach (var img in losangos)
        {
            img.sprite = null;
            img.color = new Color(1, 1, 1, 0.25f);
            img.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}

