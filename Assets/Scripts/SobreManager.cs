using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; 

public class SobreManager : MonoBehaviour
{
    [Header ("Notas")]
    [SerializeField] private RawImage notes;
    public Texture[] arrNotes;

    [Header("Notas")]
    [SerializeField] private RawImage titles;
    public Texture[] arrTitles;

    [Header ("Botões")]
    public GameObject next;
    public GameObject back;

    // Update is called once per frame
    public void GoNext()
    {
        notes.texture = arrNotes[1];
        titles.texture = arrTitles[1];
        next.SetActive(false);
        back.SetActive(true);
    }

    public void GoBack()
    {
        notes.texture = arrNotes[0];
        titles.texture = arrTitles[0];
        next.SetActive(true);
        back.SetActive(false);
    }
}
