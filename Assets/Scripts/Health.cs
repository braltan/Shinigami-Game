using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    [SerializeField]
    private int health;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
           if(PlayerVariables.lives < health)
            gameObject.GetComponent<Image>().enabled = false;

    }
}
