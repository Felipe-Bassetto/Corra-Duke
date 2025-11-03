using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInfo
{
    public string nome;
    public string descricao;
    public Sprite sprite;

    public ItemInfo(string nome, string descricao, Sprite sprite = null)
    {
        this.nome = nome;
        this.descricao = descricao;
        this.sprite = sprite;
    }
}

public class AlmanaqueData : MonoBehaviour
{
    public GameDb db;
    public Sprite defaultSprite;

    private Dictionary<string, List<ItemInfo>> categorias = new Dictionary<string, List<ItemInfo>>();

    void Awake()
    {
        // Monta as categorias e carrega do banco
        categorias["Fellas"] = new List<ItemInfo>();
        categorias["Foes"] = new List<ItemInfo>();
        categorias["Inventory"] = new List<ItemInfo>();

        // Ele busca os itens do banco
        var fellas = db.db.Table<AlmanaqueTable>().Where(x => x.NameItem.StartsWith("Fellas")).ToList();
        foreach (var f in fellas)
            categorias["Fellas"].Add(new ItemInfo(f.NameItem, f.Descricao, defaultSprite));

        var foes = db.db.Table<AlmanaqueTable>().Where(x => x.NameItem.StartsWith("Foes")).ToList();
        foreach (var e in foes)
            categorias["Foes"].Add(new ItemInfo(e.NameItem, e.Descricao, defaultSprite));

        var inv = db.db.Table<AlmanaqueTable>().Where(x => x.NameItem.StartsWith("Inventory")).ToList();
        foreach (var i in inv)
            categorias["Inventory"].Add(new ItemInfo(i.NameItem, i.Descricao, defaultSprite));
    }

    public Dictionary<string, List<ItemInfo>> GetCategorias()
    {
        return categorias;
    }
}
