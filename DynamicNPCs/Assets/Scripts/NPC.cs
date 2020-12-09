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
        if(currentRelationship <= 50)
        {
            sentancesSet = false;
            talkStranger();
        } else if (currentRelationship <= 60)
        {
            //use aquantince dialogue
            sentancesSet = false;
            talkAquantince();
        } else if (currentRelationship <= 80)
        {
            //use friend dialouge
            sentancesSet = false;
            talkFriend();
        } else if (currentRelationship <= 100)
        {
            //use great friend dialogue
            sentancesSet = false;
            talkGreatFriend();
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
        if (!sentancesSet)
        {
            dialogue.sentances.Clear();
            dialogue.sentances.Add("Hi there" + player.name);
            dialogue.sentances.Add("What have you been up to recently?");

            foreach (string sentance in talkQuest())
            {
                dialogue.sentances.Add(sentance);
            }

            dialogue.sentances.Add("Talk to you later then!");
            sentancesSet = true;
        }
    }

    void talkFriend()
    {
        if (!sentancesSet)
        {
            dialogue.sentances.Clear();
            dialogue.sentances.Add("Hey" + player.name + "! How are you doing?");
            dialogue.sentances.Add("Hope you've been doing well on your journeys");

            foreach (string sentance in talkQuest())
            {
                dialogue.sentances.Add(sentance);
            }

            dialogue.sentances.Add("See you around, good luck out there!");
            sentancesSet = true;
        }
    }

    void talkGreatFriend()
    {
        if (!sentancesSet)
        {
            dialogue.sentances.Clear();
            dialogue.sentances.Add(player.name + "! Nice to see you!");
            dialogue.sentances.Add("You mind hanging out with me for a bit?");
            dialogue.sentances.Add("I've just got a lot I want to talk about with you.");

            foreach (string sentance in talkQuest())
            {
                dialogue.sentances.Add(sentance);
            }

            dialogue.sentances.Add("Alrighty then, see you around soon friend!");
            sentancesSet = true;
        }
    }
}
