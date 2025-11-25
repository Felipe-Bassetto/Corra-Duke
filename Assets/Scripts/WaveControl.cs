using UnityEngine;

public class WaveControl : MonoBehaviour
{

    [Header("Animation")]
    private Animator anim;
    private string status = "None";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void StartAnimation()
    {
        if (status == "None")
        {
            status = "Destroyer";
            anim.Play("Destroyer");
        }
        
    }

    public void StopAnimation()
    {
        if (status != "None")
        {
            anim.Play("Parado");
        }
    }
}
