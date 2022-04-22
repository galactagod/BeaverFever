using Godot;
using System;
using System.Collections.Generic;


public class PlayerData : Node
{
    #region Variables
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
    public int Muny = 0;

    // Vars to hold equipment effects
    public int attackAdd = 0;
    public int defenseAdd = 0;
    public int spAttackAdd = 0;
    public int spDefenseAdd = 0;
    public int healthAdd = 0;
    public int staminaAdd = 0;

    public float attackScale = 1;
    public float defenseScale = 1;
    public float spAttackScale = 1;
    public float spDefenseScale = 1;
    public float healthScale = 1;
    public float staminaScale = 1;


    //vars to hold final equipment effects
    public int attackFinal = 0;
    public int defenseFinal = 0;
    public int spAttackFinal = 0;
    public int spDefenseFinal = 0;
    public int staminaFinal = 0;
    public int healthFinal = 0;

    //vars to hold the level of skills
    public int bubbleBurstSkill = 0;
    public int sliceSkill = 0;
    public int crunchSkill = 0;
    public int aegisSkill = 0;
    public int accelerateSkill = 0;
    public int graceSkill = 0;
    public int regenerationSkill = 0;

    public int inventorySize = 30;
    public int consumableSize = 0;

    //The inventory of the player
    public List<item> inv { get; set; }

    //The list of items avaliable to the player. This will get edited when the player makes a purchase.
    public List<item> itemsAvaliable { get; set; }

    //The list of items that are avaliable (after purchase, names get removed from the list)
    public List<string> itemsInStore { get; set; }

    //The equipment that the user has on
    public Dictionary<string, item> equipment { get; set; }

    public List<item> skills { get; set; }

    //The route of assets just so I can change it later if need be
    private string assetRoute = "res://assets/";

    [Signal]
    public delegate void itemRemoved();

    private PlayerStats playerStats;
    #endregion

    #region Item Classes
    public class item
    {
        public string name;
        public int price;
        public Texture texture;
        public Vector2 scale;
        public bool equippable;
        public string equippedSlot;
        public int inventorySlot;
        public string ableToBeEquippedSlot;
        public int level;
        public string textureRoute;
        //adding for skills
        public string type;
        public string tooltip;

        public List<string> whichStat = new List<string>();
        public List<string> operatorOnStat = new List<string>();
        public List<string> amountOnStat = new List<string>();

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
        public string ableToBeEquippedSlot;
        public string comingFrom;
        public string type;
        public int level;
        public string textureRoute;
        public string tooltip;

        public List<string> whichStat = new List<string>();
        public List<string> operatorOnStat = new List<string>();
        public List<string> amountOnStat = new List<string>();

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
            ableToBeEquippedSlot = another.ableToBeEquippedSlot;
            type = another.type;
            level = another.level;
            textureRoute = another.textureRoute;
            tooltip = another.tooltip;
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
            temp.ableToBeEquippedSlot = ableToBeEquippedSlot;
            temp.type = type;
            temp.level = level;
            temp.textureRoute = textureRoute;
            temp.tooltip = tooltip;
            return temp;
        }
    }
    #endregion
    
    #region Godot Overrides
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerStats = (PlayerStats)GetNode("/root/PlayerStats");
        //File IO
        //add a statement to actually write the file in if its not there
        string filepath = "user://playerStatsFile.json";
        Godot.File files = new Godot.File();
        Godot.Collections.Dictionary ParsedData = new Godot.Collections.Dictionary();
        if(files.FileExists(filepath))
        {
            files.Open(filepath, Godot.File.ModeFlags.ReadWrite);
            files.Seek(0);
            string text = files.GetAsText();
            var jsonFile = JSON.Parse(text).Result;
            ParsedData = jsonFile as Godot.Collections.Dictionary;
            files.Close();
        }
        else
        {
            files.Open(filepath, Godot.File.ModeFlags.WriteRead);
            files.Seek(0);

            Godot.Collections.Dictionary jsonToWrite = new Godot.Collections.Dictionary();
            jsonToWrite.Add("Attack", "0");
            jsonToWrite.Add("Defense", "0");
            jsonToWrite.Add("SpAttack", "0");
            jsonToWrite.Add("SpDefense", "0");
            jsonToWrite.Add("Stamina", "0");
            jsonToWrite.Add("Health", "0");
            jsonToWrite.Add("StatPoints", "0");
            jsonToWrite.Add("Muny", "0");
            Godot.Collections.Array inventory = new Godot.Collections.Array();
            jsonToWrite.Add("inventory", inventory);
            Godot.Collections.Array skillsList = new Godot.Collections.Array();
            jsonToWrite.Add("skills", skillsList);
            Godot.Collections.Array itemsAvaliable = new Godot.Collections.Array();
            foreach(var item in Global.itemsAvaliable)
            {
                itemsAvaliable.Add(item.name);
            }
            jsonToWrite.Add("itemsAvaliable", itemsAvaliable);
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
        Muny = Int32.Parse((string)ParsedData["Muny"]);

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
            temp.ableToBeEquippedSlot = (string)item["ableToBeEquippedSlot"];
            temp.tooltip = (string)item["tooltip"];
            foreach (Godot.Collections.Dictionary statEffect in (Godot.Collections.Array)item["itemEffects"])
            {
                temp.whichStat.Add((string)statEffect["stat"]);
                temp.operatorOnStat.Add((string)statEffect["operator"]);
                temp.amountOnStat.Add((string)statEffect["amount"]);
            }
            inv.Add(temp);
        }

        skills = new List<item>();
        foreach (Godot.Collections.Dictionary item in (Godot.Collections.Array)ParsedData["skills"])
        {
            item temp = new item();
            temp.name = (string)item["name"];
            temp.price = Int32.Parse((string)item["price"]);
            temp.textureRoute = (string)item["textureRoute"];
            temp.texture = (Texture)GD.Load(temp.textureRoute);
            Vector2 tempVector = new Vector2();
            tempVector.x = Int32.Parse((string)item["scaleX"]);
            tempVector.y = Int32.Parse((string)item["scaleY"]);
            temp.scale = tempVector;
            temp.equippable = Boolean.Parse((string)item["equippable"]);
            temp.equippedSlot = (string)item["equippedSlot"];
            temp.inventorySlot = Int32.Parse((string)item["inventorySlot"]);
            temp.level = Int32.Parse((string)item["level"]);
            temp.ableToBeEquippedSlot = (string)item["ableToBeEquippedSlot"];
            temp.tooltip = (string)item["tooltip"];
            foreach (Godot.Collections.Dictionary statEffect in (Godot.Collections.Array)item["itemEffects"])
            {
                temp.whichStat.Add((string)statEffect["stat"]);
                temp.operatorOnStat.Add((string)statEffect["operator"]);
                temp.amountOnStat.Add((string)statEffect["amount"]);
            }
            skills.Add(temp);
            SettingSkillLevels(temp.name, temp.level);
        }



        itemsInStore = new List<string>();
        foreach (var itemName in (Godot.Collections.Array)ParsedData["itemsAvaliable"])
        {
            itemsInStore.Add((string)itemName);
            
        }

        equipment = new Dictionary<string, item>();

        //Optimize runtime on this
        itemsAvaliable = new List<item>();
        foreach (var name in itemsInStore)
        {
            itemsAvaliable.Add(Global.itemsAvaliable.Find(s => s.name == name));
        }
        
        //iterate through inventory and see if there is anything equipted, if so add it to the dictionary
        foreach(var item in inv)
        {
            if (item.equippedSlot != "none")
            {
                equipment.Add(item.equippedSlot, item);
                EquipChangesStatFilter(item, false);
            }
        }

        foreach (var item in skills)
        {
            if (item.equippedSlot != "none")
            {
                equipment.Add(item.equippedSlot, item);
                EquipChangesStatFilter(item, false);
            }
        }

        playerStats.Muny = Muny;
        playerStats.Exp = PlayerTotalPoints;
        playerStats.ChangeExp(0);
        playerStats.ChangeMaxHealth(PlayerHealth);
        RefreshStatFinals();




        //Purely for testing TAKE OUT
        //foreach(var item in Global.itemTemplates)
        //{
        //    item.inventorySlot = inv.Count;
        //    inv.Add(item);
        //}
    }
    #endregion

    #region Equipment Stat Changes
    public void EquipChangesStatFilter(item temp, bool undo)
    {
        for (int i = 0; i < temp.whichStat.Count; i++)
        {
            switch (temp.whichStat[i])
            {
                case "Attack":
                    if (temp.operatorOnStat[i] == "/" || temp.operatorOnStat[i] == "*")
                    {
                        attackScale = ScaleHelper(attackScale, float.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    }
                    else
                        attackAdd = AddHelper(attackAdd, Int32.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    break;
                case "Defense":
                    if (temp.operatorOnStat[i] == "/" || temp.operatorOnStat[i] == "*")
                    {
                        defenseScale = ScaleHelper(defenseScale, float.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    }
                    else
                        defenseAdd = AddHelper(defenseAdd, Int32.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    break;
                case "SpAttack":
                    if (temp.operatorOnStat[i] == "/" || temp.operatorOnStat[i] == "*")
                    {
                        spAttackScale = ScaleHelper(spAttackScale, float.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    }
                    else
                        spAttackAdd = AddHelper(spAttackAdd, Int32.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    break;
                case "SpDefense":
                    if (temp.operatorOnStat[i] == "/" || temp.operatorOnStat[i] == "*")
                    {
                        spDefenseScale = ScaleHelper(spDefenseScale, float.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    }
                    else
                        spDefenseAdd = AddHelper(spDefenseAdd, Int32.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    break;
                case "Stamina":
                    if (temp.operatorOnStat[i] == "/" || temp.operatorOnStat[i] == "*")
                    {
                        staminaScale = ScaleHelper(staminaScale, float.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    }
                    else
                        staminaAdd = AddHelper(staminaAdd, Int32.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    break;
                case "Health":
                    if (temp.operatorOnStat[i] == "/" || temp.operatorOnStat[i] == "*")
                    {
                        healthScale = ScaleHelper(healthScale, float.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    }
                    else
                        healthAdd = AddHelper(healthAdd, Int32.Parse(temp.amountOnStat[i]), temp.operatorOnStat[i], undo);
                    break;
                default:
                    break;
            }
        }

        RefreshStatFinals();
    }

    public float ScaleHelper(float statToChange, float valOfEquip, string operation, bool undo)
    {
        if (operation == "/" || (operation == "*" && undo))
        {
            return statToChange / valOfEquip;
        }
        return statToChange * valOfEquip;
    }

    public int AddHelper(int statToChange, int valOfEquip, string operation, bool undo)
    {
        if (operation == "-" || (operation == "+" && undo))
        {
            return statToChange - valOfEquip;
        }
        return statToChange + valOfEquip;
    }
    #endregion

    #region Helper Methods
    public void printEquipment()
    {
        var e = equipment.Values;
        foreach (var item in e)
        {
            Console.WriteLine(item.name);
            Console.WriteLine(item.inventorySlot);
            Console.WriteLine(item.equippedSlot);
        }
    }
    public string getStatLine(item temp)
    {
        string statLine = "";
        statLine = statLine + temp.name + "\n";
        statLine = statLine + temp.ableToBeEquippedSlot + "\n";
        if(temp.type == "skill")
        {
            statLine = statLine + "Level: " + temp.level + "\n";
        }
        for (int i = 0; i < temp.amountOnStat.Count; i++)
        {
            statLine = statLine + temp.whichStat[i] + " " + temp.operatorOnStat[i] + " " + temp.amountOnStat[i] + "\n";
        }
        if(temp.equippedSlot != "none")
        {
            statLine = statLine + "Equipped" + "\n";
        }
        if(temp.tooltip != null)
        {
            statLine = statLine + temp.tooltip;
        }
        return statLine;
    }


    public void RefreshStatFinals()
    {
        attackFinal = (int)((PlayerAttack + attackAdd) * attackScale);
        defenseFinal = (int)((PlayerDefense + defenseAdd) * defenseScale);
        spAttackFinal = (int)((PlayerSpAttack + spAttackAdd) * spAttackScale);
        spDefenseFinal = (int)((PlayerSpDefense + spDefenseAdd) * spDefenseScale);
        staminaFinal = (int)((PlayerStamina + staminaAdd) * staminaScale);
        healthFinal = (int)((PlayerHealth + healthAdd) * healthScale);


        attackFinal = attackFinal < PlayerAttack ? PlayerAttack : attackFinal;
        defenseFinal = defenseFinal < PlayerDefense ? PlayerDefense : defenseFinal;
    }

    public void RemoveFromInv(int index)
    {
        inv.RemoveAt(index);
        int counter = 0;
        foreach(var item in inv)
        {
            item.inventorySlot = counter;
            counter++;
        }
        EmitSignal("itemRemoved");
    }

    public void ResetInv()
    {
        EmitSignal("itemRemoved");
    }

    public int CurrentStatPoints()
    {
        return PlayerAttack + PlayerDefense + PlayerHealth + PlayerSpAttack + PlayerSpDefense + PlayerStamina;
    }


    public int EXPNeeded(int x)
    {
        if (x < 12)
            return 500;
        //accurate level 12 and up
        return (int)(0.02 * x * x * x + 3.06 * x * x + 105.6 * x - 895);
    }

    public void SettingSkillLevels(string skillName, int level)
    {
        if (skillName == "Body Mod")
            aegisSkill = level;
        else if (skillName == "Attack Mod")
            bubbleBurstSkill = level;
        else if (skillName == "Rip Mod")
            sliceSkill = level;
        else if (skillName == "Sharp Mod")
            crunchSkill = level;
        else if(skillName == "Boots Mod")
            accelerateSkill = level;
        else if(skillName == "Book Mod")
            graceSkill = level;
        else if(skillName == "Moon Mod")
            regenerationSkill = level;
        else if (skillName == "Leaf Mod")
            regenerationSkill = level;

    }

    public void skillBought(string skillName, int levelBought)
    {
        //if the levelBought == 1, then just add it to the skills
        if(levelBought == 1)
        {
            var pulledSkill = Global.skillsAvaliable.Find(x => x.name == skillName && x.level == levelBought);
            pulledSkill.inventorySlot = skills.Count;
            skills.Add(pulledSkill);
        }
        else
        {
            var skillToChange = skills.Find(x => x.name == skillName && x.level == levelBought - 1);
            skillToChange.level = levelBought;

            item oldSkill = new item();
            oldSkill.amountOnStat = skillToChange.amountOnStat;
            oldSkill.operatorOnStat = skillToChange.operatorOnStat;
            oldSkill.whichStat = skillToChange.whichStat;

            var pullingSkills = Global.skillsAvaliable.FindAll(x => x.name == skillName);
            var skillPulled = pullingSkills[levelBought - 1];
            
            skillToChange.amountOnStat = skillPulled.amountOnStat;
            skillToChange.texture = skillPulled.texture;
            skillToChange.textureRoute = skillPulled.textureRoute;
            skillToChange.tooltip = skillPulled.tooltip;

            if (skillToChange.equippedSlot != "none")
            {
                EquipChangesStatFilter(oldSkill, true);
                EquipChangesStatFilter(skillToChange, false);
                equipment.Remove(skillToChange.equippedSlot);
                equipment.Add(skillToChange.equippedSlot, skillToChange);
            }

            skills[skillToChange.inventorySlot] = skillToChange;

        }
    }
    #endregion
}
