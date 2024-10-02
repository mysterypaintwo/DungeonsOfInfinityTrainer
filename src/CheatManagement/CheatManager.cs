using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace DungeonsOfInfinityTrainer.CheatManagement
{
    public class CheatManager
    {
        private const int TotalInventorySlots = 32;
        private const int UsedInventorySlots = 18;
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
            ARROW_COUNT,
            BOMB_BAG_BOMB_COUNT,
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
            //Inventory Groups
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
            CheatGroup groupInventory = new CheatGroup("Inventory");
            _groupMaster.AddChildGroup(groupPlayerInfo, GroupList.G_PLAYER_INFO);
            _groupMaster.AddChildGroup(groupFlags, GroupList.G_FLAGS);
            _groupMaster.AddChildGroup(groupInventory, GroupList.G_INVENTORY);

            CheatGroup groupInvSlot0 = new CheatGroup("Slot 0 (Starting Slot For Bag Upgrade)");
            groupInventory.AddChildGroup(groupInvSlot0, GroupList.G_INV_SLOT_0);
            CheatGroup groupInvSlot1 = new CheatGroup("Slot 1 (Starting Slot For No Bag Upgrade)");
            groupInventory.AddChildGroup(groupInvSlot1, GroupList.G_INV_SLOT_1);

            CheatGroup[] groupInvSlots = new CheatGroup[TotalInventorySlots];
            groupInvSlots[0] = groupInvSlot0;
            groupInvSlots[1] = groupInvSlot1;

            for (var i = 2; i < TotalInventorySlots; i++)
            {
                string usedString = string.Empty;
                if (i >= UsedInventorySlots)
                    usedString = " (Unused)";
                CheatGroup thisCheatGroup = new CheatGroup(string.Format("Slot {0}" + usedString, i));
                groupInvSlots[i] = thisCheatGroup;
                groupInventory.AddChildGroup(groupInvSlots[i], GroupList.G_INV_SLOT_0 + i);
            }

            /// Define the skeleton cheats to base all the other codes off of
            // X-Position
            Cheat xPosAddress = new Cheat("X-Pos (Ctrl + A/D to teleport)", 0x0138D550, VarType.FLOAT);
            xPosAddress.AddOffset(0x0);
            xPosAddress.AddOffset(0xD30);
            xPosAddress.AddOffset(0x18);
            xPosAddress.AddOffset(0x58);
            xPosAddress.AddOffset(0x10);
            xPosAddress.AddOffset(0xF4);
            xPosAddress.AddHotkey(HotkeyActions.DEC_VAL, new List<int>() { 17, 65 }, 32);
            xPosAddress.AddHotkey(HotkeyActions.INC_VAL, new List<int>() { 17, 68 }, 32);

            // Rupees
            /*
            Cheat rupeesAddress = new Cheat("Rupees", 0x0137EEF0, VarType.DOUBLE);
            rupeesAddress.AddOffset(0x10);
            rupeesAddress.AddOffset(0xB30);
            rupeesAddress.AddOffset(0x188);
            rupeesAddress.AddOffset(0x8);
            rupeesAddress.AddOffset(0x5B0);
            rupeesAddress.AddOffset(0x2F0);
            rupeesAddress.AddOffset(0x5C0);
            */
            Cheat rupeesAddress = new Cheat("Rupees", 0x015A2248, VarType.DOUBLE);
            rupeesAddress.AddOffset(0x808);
            rupeesAddress.AddOffset(0x18);
            rupeesAddress.AddOffset(0x188);
            rupeesAddress.AddOffset(0x8);
            rupeesAddress.AddOffset(0x5B0);
            rupeesAddress.AddOffset(0x2F0);
            rupeesAddress.AddOffset(0x5C0);

            // Has Pendant of Health
            
            Cheat hasPendantOfHealth = new Cheat("HasPendantOfHealth", 0x0137EEF0, VarType.DOUBLE);
            hasPendantOfHealth.AddOffset(0x10);
            hasPendantOfHealth.AddOffset(0x4B0);
            hasPendantOfHealth.AddOffset(0x148);
            hasPendantOfHealth.AddOffset(0x50);
            hasPendantOfHealth.AddOffset(0xF00);
            /*
            Cheat hasPendantOfHealth = new Cheat("HasPendantOfHealth", 0x0137EEF0, VarType.DOUBLE);
            hasPendantOfHealth.AddOffset(0x10);
            hasPendantOfHealth.AddOffset(0x4B0);
            hasPendantOfHealth.AddOffset(0x288);
            hasPendantOfHealth.AddOffset(0x40);
            hasPendantOfHealth.AddOffset(0x248);
            hasPendantOfHealth.AddOffset(0x50);
            hasPendantOfHealth.AddOffset(0xF00);
             */

            // Has Master Key
            /*
            Cheat hasMasterKey = new Cheat("HasMasterKey", 0x0137EEF0, VarType.DOUBLE);
            hasMasterKey.AddOffset(0x10);
            hasMasterKey.AddOffset(0x4B0);
            hasMasterKey.AddOffset(0x288);
            hasMasterKey.AddOffset(0x8);
            hasMasterKey.AddOffset(0x70);
            */
            Cheat hasMasterKey = new Cheat("HasMasterKey", 0x0137EEF0, VarType.DOUBLE);
            hasMasterKey.AddOffset(0x10);
            hasMasterKey.AddOffset(0x9A0);
            hasMasterKey.AddOffset(0x288);
            hasMasterKey.AddOffset(0x0);
            hasMasterKey.AddOffset(0x188);
            hasMasterKey.AddOffset(0x48);
            hasMasterKey.AddOffset(0x10);

            // Bomb Bag Bomb Count
            //Cheat bombBagBombCount = new Cheat("BombCount (After Bomb Bag)", arrowCount, 0xD5E150, VarType.DOUBLE);
            Cheat bombBagBombCount = new Cheat("BombCount (After Bomb Bag)", 0x013831D8, VarType.DOUBLE);
            bombBagBombCount.AddOffset(0x188);
            bombBagBombCount.AddOffset(0x18);
            bombBagBombCount.AddOffset(0x188);
            bombBagBombCount.AddOffset(0x180);

            // Inventory Slot 2 Item Quantity
            
            Cheat slot2Quantity = new Cheat("Slot 2 Item Amount (Quantity)", 0x0137EEF0, VarType.DOUBLE);
            slot2Quantity.AddOffset(0x10);
            slot2Quantity.AddOffset(0x4B0);
            slot2Quantity.AddOffset(0x148);
            slot2Quantity.AddOffset(0x30);
            slot2Quantity.AddOffset(0x70);
            slot2Quantity.AddOffset(0x7C0);
            /*
            Cheat slot2Quantity = new Cheat("Slot 2 Item Amount (Quantity)", 0x0137EEF0, VarType.DOUBLE);
            slot2Quantity.AddOffset(0x10);
            slot2Quantity.AddOffset(0x4B0);
            slot2Quantity.AddOffset(0x120);
            slot2Quantity.AddOffset(0x148);
            slot2Quantity.AddOffset(0x10);
            slot2Quantity.AddOffset(0x40);
            slot2Quantity.AddOffset(0x740);
            */

            Cheat slot2StorageID = new Cheat("Slot 2 Storage ID", slot2Quantity, -0x20, VarType.DOUBLE);
            Cheat slot2Enabled = new Cheat("Slot 2 Enabled", slot2Quantity, -0x10, VarType.DOUBLE);
            Cheat slot2ID = new Cheat("Slot 2 Item Class (ID)", slot2Quantity, 0x20, VarType.FOUR_BYTE);
            slot2ID.EnableInvDropdownList();
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
            Cheat playerMaxHealth = new Cheat("MaxHealth", rupeesAddress, 0x190, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerMaxHealth, CheatList.MAX_HEALTH);
            Cheat playerCurrentHealth = new Cheat("CurrentHealth", rupeesAddress, 0x1A0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerCurrentHealth, CheatList.CUR_HEALTH);
            Cheat playerMagic = new Cheat("Magic", rupeesAddress, 0xE0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerMagic, CheatList.MAGIC);
            groupPlayerInfo.AddCheatEntry(rupeesAddress, CheatList.RUPEES);
            Cheat arrowCount = new Cheat("ArrowCount", hasMasterKey, -0xA0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(arrowCount, CheatList.ARROW_COUNT);
            groupPlayerInfo.AddCheatEntry(bombBagBombCount, CheatList.BOMB_BAG_BOMB_COUNT);



            Cheat playerEquippedTunic = new Cheat("EquippedTunic", rupeesAddress, -0xD0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerEquippedTunic, CheatList.EQUIPPED_TUNIC);
            Cheat playerEquippedSword = new Cheat("EquippedSword", rupeesAddress, -0x80, VarType.FOUR_BYTE);
            groupPlayerInfo.AddCheatEntry(playerEquippedSword, CheatList.EQUIPPED_SWORD);
            Cheat playerEquippedShield = new Cheat("EquippedShield", rupeesAddress, -0x20, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerEquippedShield, CheatList.EQUIPPED_SHIELD);
            Cheat playerEquippedGlove = new Cheat("EquippedGlove", rupeesAddress, 0x1D0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerEquippedGlove, CheatList.EQUIPPED_GLOVE);
            Cheat playerEquippedLamp = new Cheat("EquippedLamp", rupeesAddress, 0x120, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerEquippedLamp, CheatList.EQUIPPED_LAMP);

            Cheat playerBagUpgrade = new Cheat("BagUpgrade", rupeesAddress, 0x2E0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerBagUpgrade, CheatList.BAG_UPGRADE);

            // Pendant Codes
            groupFlags.AddCheatEntry(hasPendantOfHealth, CheatList.FLAG_PEN_HEALTH);
            Cheat hasPendantOfMagic = new Cheat("HasPendantOfMagic", hasPendantOfHealth, 0x10, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfMagic, CheatList.FLAG_PEN_MAGIC);
            Cheat hasPendantOfProtection = new Cheat("HasPendantOfProtection", hasPendantOfHealth, 0x20, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfProtection, CheatList.FLAG_PEN_PROTECTION);
            Cheat hasPendantOfRevival = new Cheat("HasPendantOfRevival", hasPendantOfHealth, 0x30, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfRevival, CheatList.FLAG_PEN_REVIVAL);
            Cheat hasPendantOfDirection = new Cheat("HasPendantOfDirection", hasPendantOfHealth, 0x40, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfDirection, CheatList.FLAG_PEN_DIRECTION);
            Cheat hasPendantOfSecrets = new Cheat("HasPendantOfSecrets", hasPendantOfHealth, 0x50, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfSecrets, CheatList.FLAG_PEN_SECRETS);
            Cheat hasPendantOfImmunity = new Cheat("HasPendantOfImmunity", hasPendantOfHealth, 0x60, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfImmunity, CheatList.FLAG_PEN_IMMUNITY);
            Cheat hasPendantOfWealth = new Cheat("HasPendantOfWealth", hasPendantOfHealth, 0x70, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasPendantOfWealth, CheatList.FLAG_PEN_WEALTH);

            groupFlags.AddCheatEntry(hasMasterKey, CheatList.FLAG_BIG_KEY);
            Cheat hasMoonPearl = new Cheat("HasMoonPearl", hasMasterKey, 0x70, VarType.DOUBLE);
            groupFlags.AddCheatEntry(hasMoonPearl, CheatList.FLAG_MOON_PEARL);

            // Inventory Slots (all 17)
            Cheat[] inventoryStorageID = new Cheat[TotalInventorySlots];
            Cheat[] inventoryEnabled = new Cheat[TotalInventorySlots];
            Cheat[] inventoryQuants = new Cheat[TotalInventorySlots];
            Cheat[] inventoryIDs = new Cheat[TotalInventorySlots];
            Cheat[] inventorySecIDsDoub = new Cheat[TotalInventorySlots];
            Cheat[] inventorySecIDsFourByte = new Cheat[TotalInventorySlots];
            Cheat[] inventorySecIDsFloat = new Cheat[TotalInventorySlots];

            inventoryStorageID[2] = slot2StorageID;
            inventoryEnabled[2] = slot2Enabled;
            inventoryQuants[2] = slot2Quantity;
            inventoryIDs[2] = slot2ID;
            inventorySecIDsDoub[2] = slot2SecIDDoub;
            inventorySecIDsFourByte[2] = slot2SecIDFourByte;
            inventorySecIDsFloat[2] = slot2SecIDFloat;

            int offset = 0x80;
            for (int slotID = 0; slotID < TotalInventorySlots; slotID++)
            {
                if (slotID != 2)
                {
                    inventoryStorageID[slotID] = new Cheat(string.Format("Slot {0} Storage ID", slotID), slot2Quantity, offset - 0x20, VarType.DOUBLE);
                    groupInvSlots[slotID].AddCheatEntry(inventoryStorageID[slotID], CheatList.UNDEFINED);
                    inventoryEnabled[slotID] = new Cheat(string.Format("Slot {0} Enabled", slotID), slot2Quantity, offset - 0x10, VarType.DOUBLE);
                    groupInvSlots[slotID].AddCheatEntry(inventoryEnabled[slotID], CheatList.UNDEFINED);

                    inventoryQuants[slotID] = new Cheat(string.Format("Slot {0} Item Amount (Quantity)", slotID), slot2Quantity, offset, VarType.DOUBLE);
                    groupInvSlots[slotID].AddCheatEntry(inventoryQuants[slotID], CheatList.INV_SLOT_0 + slotID);

                    inventoryIDs[slotID] = new Cheat(string.Format("Slot {0} Item Class (ID)", slotID), slot2Quantity, offset + 0x20, VarType.FOUR_BYTE);
                    inventoryIDs[slotID].EnableInvDropdownList();
                    groupInvSlots[slotID].AddCheatEntry(inventoryIDs[slotID], CheatList.UNDEFINED);

                    inventorySecIDsDoub[slotID] = new Cheat(string.Format("Slot {0} Item Index (Secondary ID, Double)", slotID), slot2Quantity, offset + 0x10, VarType.DOUBLE);
                    groupInvSlots[slotID].AddCheatEntry(inventorySecIDsDoub[slotID], CheatList.UNDEFINED);
                    inventorySecIDsFourByte[slotID] = new Cheat(string.Format("Slot {0} Item Index (Secondary ID, 4 Byte)", slotID), slot2Quantity, offset + 0x10, VarType.FOUR_BYTE);
                    groupInvSlots[slotID].AddCheatEntry(inventorySecIDsFourByte[slotID], CheatList.UNDEFINED);
                    inventorySecIDsFloat[slotID] = new Cheat(string.Format("Slot {0} Item Index (Secondary ID, Float)", slotID), slot2Quantity, offset + 0x10, VarType.FLOAT);
                    groupInvSlots[slotID].AddCheatEntry(inventorySecIDsFloat[slotID], CheatList.UNDEFINED);
                }
                else
                {
                    groupInvSlots[2].AddCheatEntry(slot2StorageID, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2Enabled, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2Quantity, CheatList.INV_SLOT_2);
                    groupInvSlots[2].AddCheatEntry(slot2ID, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2SecIDDoub, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2SecIDFourByte, CheatList.UNDEFINED);
                    groupInvSlots[2].AddCheatEntry(slot2SecIDFloat, CheatList.UNDEFINED);
                }
                offset -= 0x40;
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