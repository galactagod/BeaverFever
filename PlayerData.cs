using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public class PlayerData : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    public int PlayerAttack = 0;
    public int PlayerDefense = 0;
    public int PlayerSpAttack = 0;
    public int PlayerSpDefense = 0;
    public int PlayerHealth = 0;
    public int PlayerStamina = 0;
    public int PlayerTotalPoints = 0;
    public int Wallet = 0;
    private string assetRoute = "res://assets/";

    public class item
    {
        public string name;
        public int price;
        public Texture texture;
        public Vector2 scale;
        public bool equippable;
        public string equippedSlot;
        public int inventorySlot;

        public List<string> whichStat = new List<string>();
        public List<Char> operatorOnStat = new List<Char>();
        public List<int> amountOnStat = new List<int>();

    }

    public class test : Godot.Object
    {
        public string name;
        public int price;
        public Texture texture;
        public Vector2 scale;
        public bool equiptable;
        public string equiptedSlot;
        public int inventorySlot;
        public string comingFrom;

        public List<string> whichStat = new List<string>();
        public List<Char> operatorOnStat = new List<Char>();
        public List<int> amountOnStat = new List<int>();

        public test(item another, string comingFrom)
        {
            name = another.name;
            price = another.price;
            texture = another.texture;
            scale = another.scale;
            equiptable = another.equippable;
            equiptedSlot = another.equippedSlot;
            inventorySlot = another.inventorySlot;
            whichStat = another.whichStat;
            amountOnStat = another.amountOnStat;
            operatorOnStat = another.operatorOnStat;
            this.comingFrom = comingFrom;
        }

        public item makeItem()
        {
            var temp = new item();
            temp.name = name;
            temp.price = price;
            temp.texture = texture;
            temp.scale = scale;
            temp.equippable = equiptable;
            temp.equippedSlot = equiptedSlot;
            temp.inventorySlot = inventorySlot;
            temp.whichStat = whichStat;
            temp.amountOnStat = amountOnStat;
            temp.operatorOnStat = operatorOnStat;
            return temp;
        }
    }

    //The inventory of the player
    public List<item> inv { get; set; }

    //The list of items avaliable to the player in the store. This will get edited when the player makes a purchase.
    public List<item> itemsAvaliable { get; set; }
    //The equipment that the user has on
    public Dictionary<string, item> equipment { get; set; }

    public string getStatLine(item temp)
    {
        string statLine = "";
        for (int i = 0; i < temp.amountOnStat.Count; i++)
        {
            statLine = statLine + temp.whichStat[i] + " " + temp.operatorOnStat[i].ToString() + " " + temp.amountOnStat[i].ToString() + "\n";
        }
        return statLine;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //File IO
        //add a statement to actually write the file in if its not there
        string filepath = "user://playerStatsFile.json";
        Godot.File files = new Godot.File();
        Godot.Collections.Dictionary ParsedData = new Godot.Collections.Dictionary();
        if(files.FileExists(filepath))
        {
            files.Open(filepath, Godot.File.ModeFlags.ReadWrite);
            Console.WriteLine(files.GetError().ToString());
            string text = files.GetAsText();
            var jsonFile = JSON.Parse(text).Result;
            ParsedData = jsonFile as Godot.Collections.Dictionary;
            files.Close();
        }
        else
        {
            files.Open(filepath, Godot.File.ModeFlags.WriteRead);

            Godot.Collections.Dictionary jsonToWrite = new Godot.Collections.Dictionary();
            jsonToWrite.Add("Attack", "0");
            jsonToWrite.Add("Defense", "0");
            jsonToWrite.Add("SpAttack", "0");
            jsonToWrite.Add("SpDefense", "0");
            jsonToWrite.Add("Stamina", "0");
            jsonToWrite.Add("Health", "0");
            jsonToWrite.Add("StatPoints", "0");
            jsonToWrite.Add("Wallet", "0");
            Godot.Collections.Array inventory = new Godot.Collections.Array();
            jsonToWrite.Add("inventory", inventory);
            files.StoreString(JSON.Print(jsonToWrite, "\t"));
            string text = files.GetAsText();
            var jsonFile = JSON.Parse(text).Result;
            ParsedData = jsonFile as Godot.Collections.Dictionary;

            files.Close();
        }
        



        PlayerAttack = Int32.Parse((string)ParsedData["Attack"]);
        PlayerDefense = Int32.Parse((string)ParsedData["Defense"]);
        PlayerSpAttack = Int32.Parse((string)ParsedData["SpAttack"]);
        PlayerSpDefense = Int32.Parse((string)ParsedData["SpDefense"]);
        PlayerHealth = Int32.Parse((string)ParsedData["Health"]);
        PlayerStamina = Int32.Parse((string)ParsedData["Stamina"]);
        PlayerTotalPoints = Int32.Parse((string)ParsedData["StatPoints"]);
        Wallet = Int32.Parse((string)ParsedData["Wallet"]);

        inv = new List<item>();
        foreach(Godot.Collections.Dictionary item in (Godot.Collections.Array)ParsedData["inventory"])
        {
            item temp = new item();
            temp.name = (string)item["name"];
            temp.price = Int32.Parse((string)item["price"]);
            temp.texture = (Texture)GD.Load(assetRoute + temp.name + ".png");
            Vector2 tempVector = new Vector2();
            tempVector.x = Int32.Parse((string)item["scaleX"]);
            tempVector.y = Int32.Parse((string)item["scaleY"]);
            temp.scale = tempVector;
            temp.equippable = Boolean.Parse((string)item["equippable"]);
            temp.equippedSlot = (string)item["equippedSlot"];
            temp.inventorySlot = Int32.Parse((string)item["inventorySlot"]);
            foreach(Godot.Collections.Dictionary statEffect in (Godot.Collections.Array)item["itemEffects"])
            {
                temp.whichStat.Add((string)statEffect["stat"]);
                temp.operatorOnStat.Add(Char.Parse((string)statEffect["operator"]));
                temp.amountOnStat.Add(Int32.Parse((string)statEffect["amount"]));
            }
            inv.Add(temp);
        }




        equipment = new Dictionary<string, item>();

        itemsAvaliable = new List<item>();

        item bronzeHelmet = new item();
        bronzeHelmet.name = "Bronze Helmet";
        bronzeHelmet.price = 500;
        bronzeHelmet.texture = (Texture)GD.Load(assetRoute + "Bronze Helmet.png");
        bronzeHelmet.scale.x = 1;
        bronzeHelmet.scale.y = 1;

        item dualSword = new item();
        dualSword.name = "Dual Sword";
        dualSword.price = 400;
        dualSword.texture = (Texture)GD.Load(assetRoute + "Dual Sword.png");
        dualSword.scale.x = 3;
        dualSword.scale.y = 3;

        item book = new item();
        book.name = "Book";
        book.price = 300;
        book.texture = (Texture)GD.Load(assetRoute+ "Book.png");
        book.scale.x = 1;
        book.scale.y = 1;


        item slime = new item();
        slime.name = "Slime";
        slime.price = 200;
        slime.texture = (Texture)GD.Load(assetRoute + "Slime.png");
        slime.scale.x = 1;
        slime.scale.y = 1;
        slime.amountOnStat.Add(5);
        slime.whichStat.Add("Health");
        slime.operatorOnStat.Add('+');

        

        

        itemsAvaliable.Add(dualSword);
        itemsAvaliable.Add(book);
        itemsAvaliable.Add(slime);
        

        


        //iterate through inventory and see if there is anything equipted, if so add it to the dictionary
        foreach(var item in inv)
        {
            if(item.equippedSlot != null)
            {
                equipment.Add(item.equippedSlot, item);
            }
        }

        
    }
}
