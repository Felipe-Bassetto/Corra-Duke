using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SoundManager soundManager;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("foi");
        if (CompareTag("Button"))
        {
            soundManager.SoundPlay(2);
        }
    }

    public void OnPointerExit(PointerEventData eventData) // Caso Necessário
    {
    }

}
