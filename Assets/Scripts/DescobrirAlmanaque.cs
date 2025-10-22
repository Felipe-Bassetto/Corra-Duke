using UnityEngine;

public class DescobridorAlmanaque : MonoBehaviour
{
    [Header("Nome do item no Almanaque")]
    public string nomeAlvo; 

    [Header("Tipo de descoberta")]
    public bool descobrirAoIniciar;     //  para o Duke por enquanto
    public bool descobrirAoAparecer;    //  inimigos e obstáculos

    private bool descoberto = false;

    void Start()
    {    
        // Se quiser resetar p testes 
        // PlayerPrefs.DeleteAll();

        //  marcado como "descobrir ao iniciar" 
        if (descobrirAoIniciar)
        {
            MarcarComoDescoberto();
        }
    }

    void OnBecameVisible()
    {
        // chamada automaticamente quando o objeto entra na tela da câmera
        if (descobrirAoAparecer && !descoberto)
        {
            MarcarComoDescoberto();
        }
    }

    public void MarcarComoDescoberto()
    {
        if (descoberto) return; // já foi descoberto

        // Salva o estado no PlayerPrefs
        PlayerPrefs.SetInt("Almanaque_" + nomeAlvo, 1);
        PlayerPrefs.Save();

        descoberto = true;

        Debug.Log($"[Almanaque] {nomeAlvo} descoberto!");

        //  Atualiza automaticamente os itens do Almanaque, se existirem 
        AlmanaqueItem[] todosItens = FindObjectsOfType<AlmanaqueItem>();
        foreach (var item in todosItens)
        {
            if (item.nome == nomeAlvo)
            {
                item.AtualizarEstadoPublico();
            }
        }    
    }
}

