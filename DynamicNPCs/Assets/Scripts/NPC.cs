using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string name;
    public string personality;
    public DialogueTrigger trigger;


    // Start is called before the first frame update
    void Start()
    {
        trigger = this.gameObject.GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
