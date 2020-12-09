using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phonebook : MonoBehaviour
{
    // public static variables 
    // public static Dictionary<int, int> phonebook = new Dictionary<int, int>();
    public static Dictionary<int, GameObject> phonebook = new Dictionary<int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] NPCs = GameObject.FindGameObjectsWithTag("NPC");
        foreach(GameObject NPC in NPCs) {
            phonebook.Add(NPC.GetComponent<NPC>().npc_id, NPC);
        }
        tester();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /* Returns relationship status of a given NPC 
     * Returns -1 if we could not find the NPC *** UP FOR DEBATE
     */
    public static int getRelationshipStatus(int npc_id) {
        GameObject ret;
        if (phonebook.ContainsKey(npc_id)){
            phonebook.TryGetValue(npc_id, out ret);
            return ret.GetComponent<NPC>().currentRelationship;
        } else {
            return -1;
        }
    }

    /*Adds "edit" value to status of NPC, autoset to 0 if status falls below 0
     Returns true if the status was updated
     Returns false if NPC was not found*/
    public static bool updateRelationshipStatus(int npc_id, int edit) {
        if (phonebook.ContainsKey(npc_id)) {
            GameObject NPC;
            phonebook.TryGetValue(npc_id, out NPC);
            int status = NPC.GetComponent<NPC>().currentRelationship;
            status += edit;
            status = (status < 0) ? 0 : status;
            NPC.GetComponent<NPC>().currentRelationship = status;
            return true;
        }
        return false; 
    }

    public static void tester() {
        int test1 = getRelationshipStatus(1);
        int test2 = getRelationshipStatus(2);
        int test3 = getRelationshipStatus(3);

        if (test1 == 12) {
            Debug.Log("Test 1 Passed : getRelationshipStatus()");
        } else {
            Debug.Log("Test 1 Failed : getRelationshipStatus()");
        }

        if (test2 == 0) {
            Debug.Log("Test 2 Passed : getRelationshipStatus()");
        }
        else {
            Debug.Log("Test 2 Failed : getRelationshipStatus()");
        }

        if (test3 == -1) {
            Debug.Log("Test 3 Passed : getRelationshipStatus()");
        }
        else {
            Debug.Log("Test 3 Failed : getRelationshipStatus()");
        }

        updateRelationshipStatus(1, 6);
        int test4 = getRelationshipStatus(1);

        if (test4 == 18) {
            Debug.Log("Test 4 Passed : getRelationshipStatus()");
        }
        else {
            Debug.Log("Test 4 Failed : getRelationshipStatus()");
        }
    }
}
