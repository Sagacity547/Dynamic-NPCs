using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //public string name;
    //public string personality;

    public Dialogue dialogue;
    public int currentRelationship;
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
    public string[] questSentances;
    public string[] talkQuest()
    {
        return questSentances;
    }

    //Stranger level relationship 0
    void talkStranger()
    {
        dialogue.sentances.
        dialogue.sentances[0] = "Hello there stranger";
        dialogue.sentances[1] = "What business do you happen to have with me?";

        int temp = 0;
        for(int i = 2; (i - 2) < talkQuest().Length; i++)
        {
            dialogue.sentances[i] = talkQuest()[i];
            temp = i;
        }

        dialogue.sentances[temp + 1] = "Well be seeing you around I suppose";
    }

    //Aquantince level Relationsip 1-3
    void talkAquantince()
    {
        dialogue.sentances[0] = "Hello there" + player.name;
        dialogue.sentances[1] = "How have you been doing?";
    }
}
