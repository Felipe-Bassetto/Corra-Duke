using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioSource musicSource;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CompareTag("Button"))
        {
            SoundManager soundManagerScript = musicSource.GetComponent<SoundManager>();
            soundManagerScript.SoundPlay(2);
        }
    }

    public void OnPointerExit(PointerEventData eventData) // Caso Necessário
    {
    }

}
