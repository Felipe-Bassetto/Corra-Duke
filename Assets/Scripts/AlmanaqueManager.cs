using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlmanaqueManager : MonoBehaviour
{
    [Header("Fonte de Dados")]
    public AlmanaqueData data;

    [Header("Containers (UI)")]
    public Transform categoryButtonContainer; 
    public Transform itemSlotContainer;       

    [Header("Prefabs")]
    public Button categoryButtonPrefab;  
    public Button itemSlotButtonPrefab; 

    [Header("Detalhes Centro")]
    public TMP_Text centralText;        // texto do meio
    public TMP_Text itemNameText;       // nome do item 
    public TMP_Text itemDescriptionText; // descrição do item 

    private string currentCategory = "";
    private List<Button> spawnedCategoryButtons = new List<Button>();
    private List<Button> spawnedItemSlots = new List<Button>();

    void Start()
    {
        if (data == null)
        {
            Debug.LogError("AlmanaqueManager: arraste referência do AlmanaqueData no Inspector.");
            return;
        }

        // mensagem inicial e limpar slots
        centralText.text = "Selecione uma categoria";
        LimparItemSlots();

        CriarBotoesDeCategoria();
    }

    void CriarBotoesDeCategoria()
    {
        // destrói antigos
        foreach (var b in spawnedCategoryButtons) Destroy(b.gameObject);
        spawnedCategoryButtons.Clear();

        var cats = data.GetCategorias();
        for (int i = 0; i < cats.Count; i++)
        {
            var cat = cats[i];
            var btn = Instantiate(categoryButtonPrefab, categoryButtonContainer);
            var txt = btn.GetComponentInChildren<TMP_Text>();
            if (txt != null) txt.text = cat.nomeCategoria;

            string nomeCat = cat.nomeCategoria; 
            btn.onClick.AddListener(() => OnCategoriaClicada(nomeCat));
            spawnedCategoryButtons.Add(btn);
        }
    }

    void OnCategoriaClicada(string categoria)
    {
        currentCategory = categoria;
        centralText.text = "Selecione um item";
        PreencherItemSlots(categoria);
    }

    void PreencherItemSlots(string categoria)
    {
        // limpa antigos
        foreach (var b in spawnedItemSlots) Destroy(b.gameObject);
        spawnedItemSlots.Clear();

        // encontra categoria
        var cat = data.categorias.Find(c => c.nomeCategoria == categoria);
        if (cat == null)
        {
            Debug.LogWarning("Categoria não encontrada: " + categoria);
            return;
        }

        // Preenche os slots: 
        int slotCount = 6;
        for (int i = 0; i < slotCount; i++)
        {
            var btn = Instantiate(itemSlotButtonPrefab, itemSlotContainer);
            spawnedItemSlots.Add(btn);

            Image img = btn.GetComponent<Image>();
            Button bcomp = btn.GetComponent<Button>();

            if (i < cat.itens.Count)
            {
                var item = cat.itens[i];
                if (img != null) img.sprite = item.icon != null ? item.icon : data.defaultIcon;

                // mostra detalhes no centro quando clicado
                bcomp.onClick.AddListener(() => MostrarDescricao(item));
                bcomp.interactable = true;
            }
            else
            {
                // slot vazio: mantém sprite padrão
                if (img != null) img.sprite = data.defaultIcon;
                bcomp.onClick.RemoveAllListeners();
                bcomp.interactable = false;
            }
        }
    }

    void MostrarDescricao(ItemData item)
    {
        if (item == null)
        {
            centralText.text = "Espaço vazio";
            return;
        }

        // Atualiza textos centrais 
        centralText.text = item.nome + "\n\n" + item.descricao;

        // Pode mostrar nome/descrição em campos separados
        if (itemNameText != null) itemNameText.text = item.nome;
        if (itemDescriptionText != null) itemDescriptionText.text = item.descricao;
    }

    void LimparItemSlots()
    {
        foreach (var b in spawnedItemSlots) Destroy(b.gameObject);
        spawnedItemSlots.Clear();
    }
}
