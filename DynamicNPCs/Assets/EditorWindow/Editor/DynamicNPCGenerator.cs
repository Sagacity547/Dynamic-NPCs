using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class DynamicNPCGenerator : EditorWindow
{
    //Variables that will be displayed for developer to edit
    string npcBaseName = "";
    string npcGuild = "";
    int npcRelationship = 50;
    int npcID = 1;
    GameObject npcObject;

    //Other Variables


    // Start is called before the first frame update
    void Start()
    {
        
    }

    [MenuItem("Tools/Dynamic NPC Generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DynamicNPCGenerator));
    }

    //These will be the values actually displayed and changable (depending) for developer use
    private void OnGUI()
    {
        GUILayout.Label("Generate New NPC", EditorStyles.boldLabel);

        npcBaseName = EditorGUILayout.TextField("NPC Name", npcBaseName);
        npcGuild = EditorGUILayout.TextField("NPC Guild/Group", npcGuild);
        npcRelationship = EditorGUILayout.IntField("NPC Relationship (0-100)", npcRelationship);
        npcID = EditorGUILayout.IntField("NPC ID", npcID);
        npcObject = EditorGUILayout.ObjectField("NPC Object", npcObject, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("Generate NPC"))
        {
            //generate NPC into scene
            GenerateNPC();
        }
    }

    //create an NPC given the varaibles inputed in the editor tool display
    private void GenerateNPC()
    {
        if (npcBaseName == string.Empty)
        {
            Debug.LogError("Error: Please enter a name for the NPC");
            return;
        }
        if (npcObject.GetComponent<NPC>() == null || npcObject.GetComponent<DialogueTrigger>() == null)
        {
            Debug.LogError("Error: Please ensure the GameObject has both the 'NPC' and 'DialogueTrigger' scripts attached to it");
            return;
        }

        Vector2 spawnPos = new Vector2(0f, 0f);
        GameObject newNPC = Instantiate(npcObject, spawnPos, Quaternion.identity);

        if(!newNPC.GetComponent<NPC>().addGuild(npcGuild))
        {
            Debug.LogError("Error: Please list a guild/group name that this NPC has NOT already been assigned to");
            return;
        }

        newNPC.GetComponent<NPC>().currentRelationship = (int) npcRelationship;
        newNPC.GetComponent<NPC>().dialogue.name = npcBaseName;
        newNPC.name = npcBaseName + npcID;

        npcID++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
