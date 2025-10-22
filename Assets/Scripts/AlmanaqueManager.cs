using UnityEngine;
using UnityEngine.UI;

public class AlmanaqueItem : MonoBehaviour
{
    [Header("Configurações")]
    public string nome; // nome do inimigo ou obstáculo

    [Header("Referências")]
    public Image imagemNormal;      // imagem real
    public Image imagemBloqueada;   // imagem com ?

    void Start()
    {
        AtualizarEstado();
    }

    // Atualiza a imagem exibida
    void AtualizarEstado()
    {
        bool descoberto = PlayerPrefs.GetInt("Almanaque_" + nome, 0) == 1;

        imagemNormal.gameObject.SetActive(descoberto);
        imagemBloqueada.gameObject.SetActive(!descoberto);
    }

    public void AtualizarEstadoPublico()
    {
        AtualizarEstado();
    }

    // Chama quando o jogador encontrar o inimigo/obstáculo
    public void MarcarComoDescoberto()
    {
        PlayerPrefs.SetInt("Almanaque_" + nome, 1);
        PlayerPrefs.Save();
        AtualizarEstado();
    }
}

