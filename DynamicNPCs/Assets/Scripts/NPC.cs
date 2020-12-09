using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //public string name;
    //public string personality;

    public Dialogue dialogue;
    public int currentRelationship;
    public int npc_id;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        //name = dialogue.name;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentRelationship == 0)
        {
            talkStranger();
        } else if (currentRelationship <= 3)
        {
            //use aquantince dialogue
        } else if (currentRelationship <= 6)
        {
            //use friend dialouge
        } else if (currentRelationship <= 10)
        {
            //use great friend dialogue
        }
    }

    [TextArea(3, 6)]
    public List<string> questSentances;
    public List<string> talkQuest()
    {
        return questSentances;
    }

    //Stranger level relationship 0
    private bool sentancesSet = false;
    void talkStranger()
    {
        if(!sentancesSet)
        {
            dialogue.sentances.Clear();
            dialogue.sentances.Add("Hello there stranger");
            dialogue.sentances.Add("What business do you happen to have with me?");

            foreach (string sentance in talkQuest())
            {
                dialogue.sentances.Add(sentance);
            }

            dialogue.sentances.Add("Well be seeing you around I suppose");
            sentancesSet = true;
        }
    }

    //Aquantince level Relationsip 1-3
    void talkAquantince()
    {
        
    }
}
