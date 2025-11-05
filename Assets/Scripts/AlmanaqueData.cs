using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string nome;
    public string descricao;
    public Sprite icon;

    public ItemData(string nome, string descricao, Sprite icon = null)
    {
        this.nome = nome;
        this.descricao = descricao;
        this.icon = icon;
    }
}

[System.Serializable]
public class CategoriaData
{
    public string nomeCategoria;
    public List<ItemData> itens = new List<ItemData>();
}

public class AlmanaqueData : MonoBehaviour
{
    [Header("Config (ex.: Ícones padrão)")]
    public Sprite defaultIcon;

    public List<CategoriaData> categorias = new List<CategoriaData>();

    void Awake()
    {
        // Se quiser mudar automaticamente dps / editar.
        if (categorias.Count == 0)
            PopularExemplo();
    }

    void PopularExemplo()
    {
        categorias.Clear();

        var c1 = new CategoriaData { nomeCategoria = "Fellas" };
        c1.itens.Add(new ItemData("Duke", "O personagem principal do jogo.", defaultIcon));
        c1.itens.Add(new ItemData("Aliado 1", "Companheiro do Duke.", defaultIcon));
        c1.itens.Add(new ItemData("Aliado 2", "Suporte técnico.", defaultIcon));

        var c2 = new CategoriaData { nomeCategoria = "Foes" };
        c2.itens.Add(new ItemData("Drone", "Aparece em plataformas.", defaultIcon));
        c2.itens.Add(new ItemData("LaserBot", "Inimigo que atira.", defaultIcon));
        c2.itens.Add(new ItemData("Chefão X", "Chefão de fase.", defaultIcon));

        var c3 = new CategoriaData { nomeCategoria = "Inventory" };
        c3.itens.Add(new ItemData("Moeda", "Usada para comprar upgrades.", defaultIcon));
        c3.itens.Add(new ItemData("Coração", "Recupera vida.", defaultIcon));
        c3.itens.Add(new ItemData("Power-Up", "Efeito temporário.", defaultIcon));

        categorias.Add(c1);
        categorias.Add(c2);
        categorias.Add(c3);
    }

    // Método público para o Manager obter as categorias
    public List<CategoriaData> GetCategorias()
    {
        return categorias;
    }
}
