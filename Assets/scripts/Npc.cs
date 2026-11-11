using UnityEngine;

public class Npc : MonoBehaviour
{
    public SpriteRenderer[] sr;
    
    private DayNightScript dayNightScript;

    void Start()
    {
        sr = GetComponents<SpriteRenderer>();
        dayNightScript = GetComponent<DayNightScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Change() 
    {
     if (dayNightScript.hours >= 21 && dayNightScript.hours < 6)
        {
         
        }  
    }
}
