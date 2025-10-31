using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlmanaqueManager : MonoBehaviour
{
    [Header("Referências")]
    public Image[] entitySlots; // Losangos da direita
    public TextMeshProUGUI mensagemCentral; // Texto no meio da tela

    [Header("Sprites de cada categoria")]
    public Sprite[] fellasSprites;
    public Sprite[] foesSprites;
    public Sprite[] inventorySprites;

    private Dictionary<string, Sprite[]> categorias;

    void Start()
    {
        categorias = new Dictionary<string, Sprite[]>
        {
            { "Fellas", fellasSprites },
            { "Foes", foesSprites },
            { "Inventory", inventorySprites }
        };

        // Mantém os losangos visíveis
        LimparSlots();

        // Mostra o texto inicial
        mensagemCentral.gameObject.SetActive(true);
        mensagemCentral.text = "Selecione uma categoria";
    }

    public void MostrarCategoria(string categoria)
    {
        if (!categorias.ContainsKey(categoria))
        {
            Debug.LogWarning("Categoria não encontrada: " + categoria);
            return;
        }

        mensagemCentral.gameObject.SetActive(false); // Esconde o texto central
        LimparSlots(); // limpa o antigo se tiver

        Sprite[] sprites = categorias[categoria];

        for (int i = 0; i < entitySlots.Length; i++)
        {
            if (i < sprites.Length && sprites[i] != null)
            {
                // Aqui você define o ícone sobre o losango
                entitySlots[i].sprite = sprites[i];
                entitySlots[i].color = Color.white;
            }
        }
    }

    private void LimparSlots()
    {
      
    }
}
