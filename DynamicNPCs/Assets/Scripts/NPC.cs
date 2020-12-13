using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //public string personality;

    public Dialogue dialogue;
    public int currentRelationship;
    public int npc_id;
    private GameObject player;
    public string questItem;
    private int questItemCount;
    public int questItemNeeded;
    private bool questComplete = false;

    public static List<NPC> connections = new List<NPC>();
    public static List<string> guilds = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        questItemNeeded = 3;
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

    void playerHasQuestItems()
    {

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

    private void addItem(string itemName)
    {
        if(itemName == questItem)
        {
            questItemCount++;
        }
    }


    private bool checkPlayerItems(string itemName)
    {
        bool hasItems = false;
        Player playerScript = player.GetComponent<Player>();
        hasItems = playerScript.itemAmount(itemName) > 0;
        return hasItems;
    }

    private void takeItems(string itemName)
    {
        Player playerScript = player.GetComponent<Player>();
        int itemsTaken = playerScript.removeItems(itemName);
        for(int i = 0; i < itemsTaken; i++)
        {
            addItem(itemName);
        }
    }

    // adds NPC to their connections and them to the NPCs connections 
    // returns true if NPC is not null
    public bool makeConnection(NPC connect)
    {
        if (connect)
        {
            connections.Add(connect);
            connect.GetComponent<NPC>().recieveConnection(this);
            return true;
        }
        return false;
    }

    // recieves make connection call from makeConnection()
    // returns true if connection was made 
    public bool recieveConnection(NPC connect) {
        if (connect)
        {
            connections.Add(connect);
            return true;
        }
        return false;
    }

    // returns list of all the guilds the calling variable has
    public List<string> getGuilds() {
        return guilds;
    }

    // returns if the NPC is in the guild requested
    public bool isInGuild(string guild) {
        return guilds.Contains(guild);
    }

    // adds guild to guild list
    // returns true if guild was added, false if the guild was already in list
    public bool addGuild(string guild) {
        if (!guilds.Contains(guild)){
            guilds.Add(guild);
            return true;
        }
        return false;
    }

    // returns all the guild memebers of a specfied guild
    // very greedy, should not be called often
    public List<NPC> getGuildMates(string guild) {
        return null;
    }
}
