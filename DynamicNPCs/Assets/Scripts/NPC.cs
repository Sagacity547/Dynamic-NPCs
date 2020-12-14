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
    private DialogueManager dManager;
    public string questItem;
    private int questItemCount;
    public int questItemNeeded = 3;
    private bool questComplete = false;
    private int raiseAmount = 30;
    public bool playerHasMet = false;

    public List<NPC> connections = new List<NPC>();
    public List<string> guilds = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        guilds.Add("Thieves Guild");
        connections = getGuildMatesFromConnections("Thieves Guild");
        if (connections.Count == 0)
        {
            Debug.Log("Test 5 Passed : Guild Mates from Connections");
        }
        else
        {
            Debug.Log("Test 5 Failed: Guld Mates from Connections");
        }

        connections = getGuildMatesFromPhonebook("Thieves Guild");
        if (connections.Count == 2)
        {
            Debug.Log("Test 6 Passed : Guild Mates from Connections");
        }
        else
        {
            Debug.Log("Test 6 Failed: Guld Mates from Connections");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dManager.dialogueEnded)
        {
            sentancesSet = false;
        }
        if (playerHasMet && !questComplete)
        {
            if (checkPlayerItems(questItem) && player.GetComponent<Player>().startedTalking)
            {
                playerHasItems();
                takeItems(questItem);
                questComplete = questItemCount >= questItemNeeded;
                if (questComplete)
                {
                    raiseRelationship(raiseAmount);
                }
            }
        }
        else if (currentRelationship <= 50)
        {
            talkStranger();
        }
        else if (currentRelationship <= 60)
        {
            //use aquantince dialogue
            talkAquantince();
        }
        else if (currentRelationship <= 80)
        {
            //use friend dialouge
            talkFriend();
        }
        else if (currentRelationship <= 100)
        {
            //use great friend dialogue
            talkGreatFriend();
        }
    }

    [TextArea(3, 6)]
    public List<string> questSentances;
    public List<string> talkQuest()
    {
        return questSentances;
    }

    void playerHasItems()
    {
        if (!sentancesSet)
        {
            dialogue.sentances.Clear();
            dialogue.sentances.Add("Hi there");
            dialogue.sentances.Add("I see you've brought me some " + questItem + "s for me");
            dialogue.sentances.Add("Thank you so much!");
            sentancesSet = true;
        }
    }

    //Stranger level relationship 0
    public bool sentancesSet = false;
    void talkStranger()
    {
        if (!sentancesSet)
        {
            dialogue.sentances.Clear();
            dialogue.sentances.Add("Hello there stranger");
            dialogue.sentances.Add("What business do you happen to have with me?");

            if (!questComplete)
            {
                foreach (string sentance in talkQuest())
                {
                    dialogue.sentances.Add(sentance);
                }
            }

            dialogue.sentances.Add("Well be seeing you around I suppose");
            sentancesSet = true;
        }
        playerHasMet = true;
    }

    //Aquantince level Relationsip 1-3
    void talkAquantince()
    {
        if (!sentancesSet)
        {
            dialogue.sentances.Clear();
            dialogue.sentances.Add("Hi there " + player.name);
            dialogue.sentances.Add("What have you been up to recently?");

            if (!questComplete)
            {
                foreach (string sentance in talkQuest())
                {
                    dialogue.sentances.Add(sentance);
                }
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
            dialogue.sentances.Add("Hey " + player.name + "! How are you doing?");
            dialogue.sentances.Add("Hope you've been doing well on your journeys");

            if (!questComplete)
            {
                foreach (string sentance in talkQuest())
                {
                    dialogue.sentances.Add(sentance);
                }
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

            if (!questComplete)
            {
                foreach (string sentance in talkQuest())
                {
                    dialogue.sentances.Add(sentance);
                }
            }

            dialogue.sentances.Add("Alrighty then, see you around soon friend!");
            sentancesSet = true;
        }
    }

    private void raiseRelationship(int raiseAmount)
    {
        currentRelationship += raiseAmount;
    }

    private void addItem(string itemName)
    {
        if (itemName == questItem)
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
        for (int i = 0; i < itemsTaken; i++)
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
    public bool recieveConnection(NPC connect)
    {
        if (connect)
        {
            connections.Add(connect);
            return true;
        }
        return false;
    }

    // returns list of all the guilds the calling variable has
    public List<string> getGuilds()
    {
        return guilds;
    }

    // returns if the NPC is in the guild requested
    public bool isInGuild(string guild)
    {
        return guilds.Contains(guild);
    }

    // adds guild to guild list
    // returns true if guild was added, false if the guild was already in list
    public bool addGuild(string guild)
    {
        if (!guilds.Contains(guild))
        {
            guilds.Add(guild);
            return true;
        }
        return false;
    }

    // returns all the guild memebers of a specfied guild
    // very greedy, should not be called often
    public List<NPC> getGuildMates(string guild)
    {
        return null;
    }

    // returns all the guild memebers of a specfied guild from NPCs connections
    public List<NPC> getGuildMatesFromConnections(string guild)
    {
        List<NPC> ret = new List<NPC>();
        foreach (NPC npc in connections)
        {
            if (npc.guilds.Contains(guild))
            {
                ret.Add(npc);
            }
        }
        return ret;
    }

    // returns all NPCs in scene phonebook that are in "guild"
    // returns null if phonebook has not be initalized 
    public List<NPC> getGuildMatesFromPhonebook(string guild)
    {
        GameObject phonebook = GameObject.FindGameObjectWithTag("phonebook");
        if (!phonebook)
            return null;
        List<NPC> ret = new List<NPC>();
        ret = phonebook.GetComponent<Phonebook>().getGuildMembers(guild);
        return ret;
    }
}
