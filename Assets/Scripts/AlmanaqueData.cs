using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AlmanaqueItem
{
    public string nome;
    public string categoria;
    public string descricao;

    
    public Sprite spriteIcon;      // Ícone 
    public Sprite spriteGrande;    // Imagem colorida 
}

[CreateAssetMenu(fileName = "AlmanaqueData", menuName = "DukeGo/Almanaque Data")]
public class AlmanaqueData : ScriptableObject
{
    [Header("Itens do Almanaque")]
    public List<AlmanaqueItem> todosOsItens = new List<AlmanaqueItem>();

    public List<AlmanaqueItem> ObterItensPorCategoria(string categoria)
    {
        return todosOsItens.FindAll(item => item.categoria == categoria);
    }
}


