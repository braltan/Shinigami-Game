using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Orb : MonoBehaviour
{
    [SerializeField]
    private string memoryText;
    [SerializeField]
    private int point;
    private bool isCollected = false;
    
    private enum OrbType
    {
        Ascension,
        Descension
    };
    [SerializeField]
    OrbType orbType;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.transform.tag == "Player")
        {
            isCollected = true;
            if (orbType == OrbType.Ascension)
            {
                Shinigami.ascensionPoints += point;
                
             
            }
            else if (orbType == OrbType.Descension)
            {
                Shinigami.descensionPoints += point;
               
            }
        }
    }
    public string getMemoryText()
    {
        return memoryText;
    }
    public bool getIsCollected()
    {
        return isCollected;
    }
}
