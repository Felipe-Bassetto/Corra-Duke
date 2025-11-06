using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AlmanaqueItem
{
    public string nome;
    public string categoria;
    public string descricao;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "AlmanaqueData", menuName = "DukeGo/Almanaque Data")]
public class AlmanaqueData : ScriptableObject
{
    [Header("Itens do Almanaque")]
    public List<AlmanaqueItem> todosOsItens = new List<AlmanaqueItem>();

    // Retorna todos os itens de uma categoria
    public List<AlmanaqueItem> ObterItensPorCategoria(string categoria)
    {
        return todosOsItens.FindAll(item => item.categoria == categoria);
    }
}
