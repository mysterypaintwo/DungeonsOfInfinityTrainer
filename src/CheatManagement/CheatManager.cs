using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace DungeonsOfInfinityTrainer.CheatManagement
{
    public class CheatManager
    {
        private const int NegInventoryEntries = 5;
        private const int UsedInventorySlots = 18;
        private const int TotalInventorySlots = UsedInventorySlots + NegInventoryEntries + 13;
        private static int s_xmlID = 0;
        private static int s_groupCount = 1;
        private CheatGroup _groupMaster;

        public enum CheatList
        {
            UNDEFINED = -1,
            //Group Player info
            X_POS,
            Y_POS,
            RUPEES,
            MAX_HEALTH,
            CUR_HEALTH,
            MAGIC,
            KEY_COUNT,
            ARROW_COUNT,
            BOMB_BAG_BOMB_COUNT,
            EQUIPPED_ITEM_SLOT,
            EQUIPPED_TUNIC,
            EQUIPPED_SWORD,
            EQUIPPED_SHIELD,
            EQUIPPED_LAMP,
            EQUIPPED_GLOVE,
            BAG_UPGRADE,

            //Group Flags
            FLAG_PEN_HEALTH,
            FLAG_PEN_MAGIC,
            FLAG_PEN_PROTECTION,
            FLAG_PEN_REVIVAL,
            FLAG_PEN_DIRECTION,
            FLAG_PEN_SECRETS,
            FLAG_PEN_IMMUNITY,
            FLAG_PEN_WEALTH,
            FLAG_BIG_KEY,
            FLAG_MOON_PEARL,
            FLAG_OIL_LAMP,
            FLAG_INVENTORY_IS_DISABLED,

            //Inventory Groups
            INV_SLOT_NEG_7,
            INV_SLOT_NEG_6,
            INV_SLOT_NEG_5,
            INV_SLOT_NEG_4,
            INV_SLOT_NEG_3,
            INV_SLOT_NEG_2,
            INV_SLOT_NEG_1,
            INV_SLOT_0,
            INV_SLOT_1,
            INV_SLOT_2,
            INV_SLOT_3,
            INV_SLOT_4,
            INV_SLOT_5,
            INV_SLOT_6,
            INV_SLOT_7,
            INV_SLOT_8,
            INV_SLOT_9,
            INV_SLOT_10,
            INV_SLOT_11,
            INV_SLOT_12,
            INV_SLOT_13,
            INV_SLOT_14,
            INV_SLOT_15,
            INV_SLOT_16,
            INV_SLOT_17,
            MAX
        };

        public enum GroupList
        {
            G_MASTER,
            G_PLAYER_INFO,
            G_FLAGS,
            G_INVENTORY,
            G_INV_SLOT_NEG_7,
            G_INV_SLOT_NEG_6,
            G_INV_SLOT_NEG_5,
            G_INV_SLOT_NEG_4,
            G_INV_SLOT_NEG_3,
            G_INV_SLOT_NEG_2,
            G_INV_SLOT_NEG_1,
            G_INV_SLOT_0,
            G_INV_SLOT_1,
            G_INV_SLOT_2,
            G_INV_SLOT_3,
            G_INV_SLOT_4,
            G_INV_SLOT_5,
            G_INV_SLOT_6,
            G_INV_SLOT_7,
            G_INV_SLOT_8,
            G_INV_SLOT_9,
            G_INV_SLOT_10,
            G_INV_SLOT_11,
            G_INV_SLOT_12,
            G_INV_SLOT_13,
            G_INV_SLOT_14,
            G_INV_SLOT_15,
            G_INV_SLOT_16,
            G_INV_SLOT_17,
            MAX
        };

        public enum HotkeyActions
        {
            UNDEFINED,
            DEC_VAL,
            INC_VAL,
            MAX
        };
        public CheatManager()
        {
            _groupMaster = new CheatGroup(MainWindow.GameVersion + " (Current)");
            CheatGroup groupPlayerInfo = new CheatGroup("Player Information");
            CheatGroup groupFlags = new CheatGroup("Flags");
            CheatGroup groupInventory = new CheatGroup("Inventory (WIP, needs refactoring)");
            _groupMaster.AddChildGroup(groupPlayerInfo, GroupList.G_PLAYER_INFO);
            _groupMaster.AddChildGroup(groupFlags, GroupList.G_FLAGS);
            _groupMaster.AddChildGroup(groupInventory, GroupList.G_INVENTORY);

            Dictionary<int, CheatGroup> groupInvSlots = new Dictionary<int, CheatGroup>();

            for (var i = -NegInventoryEntries; i <= TotalInventorySlots - NegInventoryEntries; i++) // Starts from Slot -[NegInventoryEntries]
            {
                string usedString = string.Empty;
                switch (i)
                {
                    case 0:
                        CheatGroup groupInvSlot0 = new CheatGroup("Slot 0 (Starting Slot For Bag Upgrade)");
                        groupInventory.AddChildGroup(groupInvSlot0, GroupList.G_INV_SLOT_0);
                        groupInvSlots[0] = groupInvSlot0;
                        break;
                    case 1:
                        CheatGroup groupInvSlot1 = new CheatGroup("Slot 1 (Starting Slot For No Bag Upgrade)");
                        groupInventory.AddChildGroup(groupInvSlot1, GroupList.G_INV_SLOT_1);
                        groupInvSlots[1] = groupInvSlot1;
                        break;
                    default:
                        if (i >= UsedInventorySlots)
                            usedString = " (Unused)";
                        CheatGroup thisCheatGroup = new CheatGroup(string.Format("Slot {0}" + usedString, i));
                        groupInvSlots[i] = thisCheatGroup;
                        groupInventory.AddChildGroup(groupInvSlots[i], GroupList.G_INV_SLOT_0 + i);
                        break;
                }
            }

            /// Define the skeleton cheats to base all the other codes off of
            // X-Position
            // Setup tree position is 944
            // v1.2.0
            Cheat xPosAddress = new Cheat("X-Pos (Ctrl + A/D to teleport)", 0x0144BB20, VarType.FLOAT);
            xPosAddress.AddOffset(0x0);
            xPosAddress.AddOffset(0x10);
            xPosAddress.AddOffset(0xDA0);
            xPosAddress.AddOffset(0x18);
            xPosAddress.AddOffset(0x70);
            xPosAddress.AddOffset(0x10);
            xPosAddress.AddOffset(0xF4);
            xPosAddress.AddHotkey(HotkeyActions.DEC_VAL, new List<int>() { 17, 65 }, 32);
            xPosAddress.AddHotkey(HotkeyActions.INC_VAL, new List<int>() { 17, 68 }, 32);

            /*
            Cheat inventoryIsDisabled = new Cheat("InventoryIsDisabled", 0x01385010, VarType.DOUBLE);
            inventoryIsDisabled.AddOffset(0x160);
            inventoryIsDisabled.AddOffset(0x68);
            inventoryIsDisabled.AddOffset(0x78);
            inventoryIsDisabled.AddOffset(0x48);
            inventoryIsDisabled.AddOffset(0x10);
            inventoryIsDisabled.AddOffset(0x7B0);
            inventoryIsDisabled.AddOffset(0x690);
            groupFlags.AddCheatEntry(inventoryIsDisabled, CheatList.FLAG_INVENTORY_IS_DISABLED);
            */

            // Rupees
            // v1.2.0
            Cheat rupeesAddress = new Cheat("Rupees", 0x016B9660, VarType.DOUBLE);
            rupeesAddress.AddOffset(0x188);
            rupeesAddress.AddOffset(0x0);
            rupeesAddress.AddOffset(0x3D0);
            rupeesAddress.AddOffset(0x200);

            // Has Pendant of Health
            // v1.2.0
            Cheat hasPendantOfHealth = new Cheat("HasPendantOfHealth", 0x016BC2E0, VarType.DOUBLE);
            hasPendantOfHealth.AddOffset(0x248);
            hasPendantOfHealth.AddOffset(0x50);
            hasPendantOfHealth.AddOffset(0xC00);

            // Has Master Key
            // v1.2.0
            Cheat hasMasterKey = new Cheat("HasMasterKey", 0x016BC468, VarType.DOUBLE);
            hasMasterKey.AddOffset(0x388);
            hasMasterKey.AddOffset(0x48);
            hasMasterKey.AddOffset(0xE0);

            // Has Moon Pearl
            Cheat hasMoonPearl = new Cheat("HasMoonPearl", hasMasterKey, 0x20, VarType.DOUBLE);

            // Bomb Bag Bomb Count
            // v1.2.0
            Cheat bombBagBombCount = new Cheat("Bomb Count (After Bomb Bag)", 0x01443A00, VarType.DOUBLE);
            bombBagBombCount.AddOffset(0x188);
            bombBagBombCount.AddOffset(0x540);

            // v1.2.0
            Cheat arrowCount = new Cheat("Arrow Count", 0x016BC498, VarType.DOUBLE);
            arrowCount.AddOffset(0x288);
            arrowCount.AddOffset(0x8);
            arrowCount.AddOffset(0x0);

            // Inventory Slot 2 Item Quantity
            // v1.2.0
            Cheat slot2Quantity = new Cheat("Slot 2 Item Amount (Quantity)", 0x016BC0D8, VarType.DOUBLE);
            slot2Quantity.AddOffset(0x320);
            slot2Quantity.AddOffset(0x280);
            slot2Quantity.AddOffset(0x240);
            slot2Quantity.AddOffset(0xD0);

            //Cheat slot2StorageID = new Cheat("Slot 2 Storage ID", slot2Quantity, -0x20, VarType.DOUBLE);
            Cheat slot2Enabled = new Cheat("Slot 2 Disabled", slot2Quantity, -0x20, VarType.DOUBLE);
            Cheat slot2IDDoub = new Cheat("Slot 2 Item Class (ID, Double [Adds to Inventory])", slot2Quantity, 0x20, VarType.DOUBLE);
            slot2IDDoub.EnableInvDropdownList();
            Cheat slot2IDFourByte = new Cheat("Slot 2 Item Class (ID, 4 Byte [Modifies Inventory])", slot2Quantity, 0x20, VarType.FOUR_BYTE);
            slot2IDFourByte.EnableInvDropdownList();
            Cheat slot2SecIDDoub = new Cheat("Slot 2 Item Index (Secondary ID, Double)", slot2Quantity, 0x10, VarType.DOUBLE);
            Cheat slot2SecIDFourByte = new Cheat("Slot 2 Item Index (Secondary ID, 4 Byte)", slot2Quantity, 0x10, VarType.FOUR_BYTE);
            Cheat slot2SecIDFloat = new Cheat("Slot 2 Item Index (Secondary ID, Float)", slot2Quantity, 0x10, VarType.FLOAT);

            /// Generate all the other codes (and add the skeleton codes to the code list)

            // X/Y Position
            groupPlayerInfo.AddCheatEntry(xPosAddress, CheatList.X_POS);

            Cheat yPosAddress = new Cheat("Y-Pos (Ctrl + W/S to teleport)", xPosAddress, 0x4, VarType.FLOAT);
            yPosAddress.AddHotkey(HotkeyActions.DEC_VAL, new List<int>() { 17, 87 }, 32);
            yPosAddress.AddHotkey(HotkeyActions.INC_VAL, new List<int>() { 17, 83 }, 32);
            groupPlayerInfo.AddCheatEntry(yPosAddress, CheatList.Y_POS);

            // Player Status Codes
            Cheat playerMaxHealth = new Cheat("Max Health", rupeesAddress, 0x180, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerMaxHealth, CheatList.MAX_HEALTH);
            Cheat playerCurrentHealth = new Cheat("Current Health", playerMaxHealth, 0x10, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerCurrentHealth, CheatList.CUR_HEALTH);


            Cheat playerMagic = new Cheat("Magic (Max: 64)", rupeesAddress, 0xD0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerMagic, CheatList.MAGIC);
            groupPlayerInfo.AddCheatEntry(rupeesAddress, CheatList.RUPEES);
            //Cheat arrowCount = new Cheat("Arrow Count", hasMasterKey, -0xA0, VarType.DOUBLE);
            Cheat playerKeyCount = new Cheat("Key Count", playerMaxHealth, -0x20, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerKeyCount, CheatList.KEY_COUNT);

            groupPlayerInfo.AddCheatEntry(arrowCount, CheatList.ARROW_COUNT);
            groupPlayerInfo.AddCheatEntry(bombBagBombCount, CheatList.BOMB_BAG_BOMB_COUNT);



            //Cheat playerEquippedItemSlot = new Cheat("Equipped Item Slot (First slot is 7)", slot2Quantity, -0xBA0, VarType.DOUBLE);
            //groupPlayerInfo.AddCheatEntry(playerEquippedItemSlot, CheatList.EQUIPPED_ITEM_SLOT);

            Cheat playerEquippedTunic = new Cheat("Equipped Tunic", rupeesAddress, -0xC0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerEquippedTunic, CheatList.EQUIPPED_TUNIC);
            Cheat playerEquippedSword = new Cheat("Equipped Sword", rupeesAddress, -0x80, VarType.FOUR_BYTE);
            groupPlayerInfo.AddCheatEntry(playerEquippedSword, CheatList.EQUIPPED_SWORD);
            Cheat playerEquippedShield = new Cheat("Equipped Shield", rupeesAddress, -0x20, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerEquippedShield, CheatList.EQUIPPED_SHIELD);
            Cheat playerEquippedGlove = new Cheat("Equipped Glove", rupeesAddress, 0x1C0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerEquippedGlove, CheatList.EQUIPPED_GLOVE);

            // Has Oil Lamp
            Cheat hasOilLamp = new Cheat("HasOilLamp", playerEquippedSword, 0x190, VarType.DOUBLE);

            //Cheat playerEquippedLamp = new Cheat("Equipped Lamp (Don't freeze on New Game)", playerEquippedGlove, 0x40, VarType.DOUBLE);
            //groupPlayerInfo.AddCheatEntry(playerEquippedLamp, CheatList.EQUIPPED_LAMP);

            //Cheat playerBagUpgrade = new Cheat("BagUpgrade", playerEquippedGlove, 0x200, VarType.DOUBLE);
            //groupPlayerInfo.AddCheatEntry(playerBagUpgrade, CheatList.BAG_UPGRADE);

            // Pendant Codes
            groupFlags.AddCheatEntry(hasPendantOfHealth, CheatList.FLAG_PEN_HEALTH);
            Cheat hasPendantOfMagic = new Cheat("HasPendantOfMagic", hasPendantOfHealth, 0x10, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfMagic, CheatList.FLAG_PEN_MAGIC);
            Cheat hasPendantOfProtection = new Cheat("HasPendantOfProtection", hasPendantOfHealth, 0x20, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfProtection, CheatList.FLAG_PEN_PROTECTION);
            Cheat hasPendantOfRevival = new Cheat("HasPendantOfRevival (Doesn't work! Add➜Drop➜Grab with Inventory codes. Item Class: 34, Item Index [Double]: 3)", hasPendantOfHealth, 0x30, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfRevival, CheatList.FLAG_PEN_REVIVAL);
            Cheat hasPendantOfDirection = new Cheat("HasPendantOfDirection", hasPendantOfHealth, 0x40, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfDirection, CheatList.FLAG_PEN_DIRECTION);
            Cheat hasPendantOfSecrets = new Cheat("HasPendantOfSecrets", hasPendantOfHealth, 0x50, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfSecrets, CheatList.FLAG_PEN_SECRETS);
            Cheat hasPendantOfImmunity = new Cheat("HasPendantOfImmunity", hasPendantOfHealth, 0x60, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfImmunity, CheatList.FLAG_PEN_IMMUNITY);
            Cheat hasPendantOfWealth = new Cheat("HasPendantOfWealth", hasPendantOfHealth, 0x70, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfWealth, CheatList.FLAG_PEN_WEALTH);

            groupFlags.AddCheatEntry(hasMoonPearl, CheatList.FLAG_MOON_PEARL);
            groupFlags.AddCheatEntry(hasMasterKey, CheatList.FLAG_BIG_KEY);
            groupFlags.AddCheatEntry(hasOilLamp, CheatList.FLAG_OIL_LAMP);

            // Inventory Slots (all 17)
            //Dictionary<int, Cheat> inventoryStorageID = new Dictionary<int, Cheat>();
            Dictionary<int, Cheat> inventoryEnabled = new Dictionary<int, Cheat>();
            Dictionary<int, Cheat> inventoryQuants = new Dictionary<int, Cheat>();
            Dictionary<int, Cheat> inventoryIDsDoub = new Dictionary<int, Cheat>();
            Dictionary<int, Cheat> inventoryIDsFourByte = new Dictionary<int, Cheat>();
            Dictionary<int, Cheat> inventorySecIDsDoub = new Dictionary<int, Cheat>();
            Dictionary<int, Cheat> inventorySecIDsFourByte = new Dictionary<int, Cheat>();
            Dictionary<int, Cheat> inventorySecIDsFloat = new Dictionary<int, Cheat>();

            //inventoryStorageID[2] = slot2StorageID;
            inventoryEnabled[2] = slot2Enabled;
            inventoryQuants[2] = slot2Quantity;
            inventoryIDsDoub[2] = slot2IDDoub;
            inventoryIDsFourByte[2] = slot2IDFourByte;
            inventorySecIDsDoub[2] = slot2SecIDDoub;
            inventorySecIDsFourByte[2] = slot2SecIDFourByte;
            inventorySecIDsFloat[2] = slot2SecIDFloat;

            int slotOffsetCount = 2 + NegInventoryEntries;
            int offset = 0x80 * slotOffsetCount; // i = 0
            for (int slotID = -NegInventoryEntries; slotID <= TotalInventorySlots - NegInventoryEntries; slotID++) // Start from -[NegInventoryEntries]
            {
                if (slotID != 2)
                {
                    inventoryEnabled[slotID] = new Cheat(string.Format("Slot {0} Disabled", slotID), slot2Quantity, offset - 0x20, VarType.DOUBLE);
                    groupInvSlots[slotID].AddCheatEntry(inventoryEnabled[slotID], CheatList.UNDEFINED);

                    inventoryQuants[slotID] = new Cheat(string.Format("Slot {0} Item Amount (Quantity)", slotID), slot2Quantity, offset, VarType.DOUBLE);
                    groupInvSlots[slotID].AddCheatEntry(inventoryQuants[slotID], CheatList.INV_SLOT_0 + slotID);

                    inventoryIDsDoub[slotID] = new Cheat(string.Format("Slot {0} Item Class (ID, Double [Adds to Inventory])", slotID), slot2Quantity, offset + 0x20, VarType.DOUBLE);
                    inventoryIDsDoub[slotID].EnableInvDropdownList();
                    groupInvSlots[slotID].AddCheatEntry(inventoryIDsDoub[slotID], CheatList.UNDEFINED);

                    inventoryIDsFourByte[slotID] = new Cheat(string.Format("Slot {0} Item Class (ID, 4 Byte [Modifies Inventory])", slotID), slot2Quantity, offset + 0x20, VarType.FOUR_BYTE);
                    inventoryIDsFourByte[slotID].EnableInvDropdownList();
                    groupInvSlots[slotID].AddCheatEntry(inventoryIDsFourByte[slotID], CheatList.UNDEFINED);

                    inventorySecIDsDoub[slotID] = new Cheat(string.Format("Slot {0} Item Index (Secondary ID, Double)", slotID), slot2Quantity, offset + 0x10, VarType.DOUBLE);
                    groupInvSlots[slotID].AddCheatEntry(inventorySecIDsDoub[slotID], CheatList.UNDEFINED);
                    inventorySecIDsFourByte[slotID] = new Cheat(string.Format("Slot {0} Item Index (Secondary ID, 4 Byte)", slotID), slot2Quantity, offset + 0x10, VarType.FOUR_BYTE);
                    groupInvSlots[slotID].AddCheatEntry(inventorySecIDsFourByte[slotID], CheatList.UNDEFINED);
                    inventorySecIDsFloat[slotID] = new Cheat(string.Format("Slot {0} Item Index (Secondary ID, Float)", slotID), slot2Quantity, offset + 0x10, VarType.FLOAT);
                    groupInvSlots[slotID].AddCheatEntry(inventorySecIDsFloat[slotID], CheatList.UNDEFINED);
                }
                else
                {
                    //groupInvSlots[2].AddCheatEntry(slot2StorageID, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2Enabled, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2Quantity, CheatList.INV_SLOT_2);
                    groupInvSlots[2].AddCheatEntry(slot2IDDoub, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2IDFourByte, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2SecIDDoub, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2SecIDFourByte, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2SecIDFloat, CheatList.UNDEFINED);
                }
                offset -= 0x80;
            }
        }

        public static int GetXmlID()
        {
            return s_xmlID;
        }

        public static void IncrementXmlID()
        {
            s_xmlID++;
        }

        public CheatGroup GetCheatTree()
        {
            return _groupMaster;
        }

        public List<string> EmitAllCheats(CheatGroup groupMaster)
        {
            List<string> output = new List<string>();
            output.Add("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            output.Add("<CheatTable CheatEngineTableVersion=\"45\">");
            output.Add("<CheatEntries>");

            groupMaster.Emit(output);

            output.Add("</CheatEntries>");
            output.Add("</CheatTable>");

            return output;
        }

        public static int GetGroupCount()
        {
            return s_groupCount;
        }

        public static void IncrementGroupCount()
        {
            s_groupCount++;
        }
    }
}