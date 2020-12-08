using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phonebook : MonoBehaviour
{
    // public static variables 
    public static Dictionary<GameObject, int> phonebook = new Dictionary<GameObject, int>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] NPCs = GameObject.FindGameObjectsWithTag("NPC");
        foreach(GameObject NPC in NPCs) {
           // Debug.Log("NPC : " + NPC.name + " Hash Code : " + NPC.GetHashCode());
           // Debug.Log("Relationship : " + NPC.GetComponent<NPC>().currentRelationship);
            phonebook.Add(NPC, NPC.GetComponent<NPC>().currentRelationship);
        }
        /*foreach(int i in phonebook.Values){
            Debug.Log("Value: " + i);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
