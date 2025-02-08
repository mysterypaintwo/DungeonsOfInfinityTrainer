using System.Collections.Generic;

namespace DungeonsOfInfinityTrainer.CheatManagement
{
    public class CheatManager
    {
        private const int NegativeInventoryEntries = 0;
        private const int UsedInventorySlots = 10;
        private const int TotalPrimaryInventorySlots = UsedInventorySlots + NegativeInventoryEntries + 0;
        private const int TreasureInventorySlots = 6;
        private const int FoodInventorySlots = 3;
        private const int PendantInventorySlots = 3;
        private const int BombInventorySlots = 1;
        private const int EquipmentInventorySlots = 6;

        private static int XmlID = 0;
        private static int GroupCount = 1;

        private int inventoryCodesCounter = 0;
        private int inventoryGroupsCounter = 0;

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
            FLAG_QUIVER,
            FLAG_BOMB_BAG,
            FLAG_FOOD_BAG,
            FLAG_TREASURE_BAG,
            FLAG_PENDANT_BAG,
            FLAG_OIL_LAMP,
            FLAG_INVENTORY_IS_DISABLED,
            FLAG_INVENTORY_NAVIGATION_FOOD,
            FLAG_INVENTORY_NAVIGATION_BOMBS,
            FLAG_INVENTORY_NAVIGATION_PENDANTS,
            FLAG_INVENTORY_NAVIGATION_TREASURES,

            // Inventory Groups (Primary)
            INV_SLOTS_BEGIN,
            MAX
        };

        public enum GroupList
        {
            G_MASTER,
            G_PLAYER_INFO,
            G_FLAGS,
            G_INVENTORY,
            G_INVENTORY_PRIMARY,
            G_INVENTORY_EQUIPMENT,
            G_INVENTORY_FOOD_BAG,
            G_INVENTORY_BOMB_BAG,
            G_INVENTORY_PENDANT_BAG,
            G_INVENTORY_TREASURE_BAG,
            G_INV_SLOTS_BEGIN,
            MAX,
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
            CheatGroup groupInventoryMaster = new CheatGroup("Inventory Codes");
            CheatGroup groupInventoryPrimary = new CheatGroup("Main Inventory Window");
            CheatGroup groupInventoryEquipment = new CheatGroup("Equipment Bag");
            CheatGroup groupInventoryFoodBag = new CheatGroup("Food Bag");
            CheatGroup groupInventoryBombBag = new CheatGroup("Bomb Bag");
            CheatGroup groupInventoryPendantBag = new CheatGroup("Pendant Bag");
            CheatGroup groupInventoryTreasureBag = new CheatGroup("Treasure Bag");
            _groupMaster.AddChildGroup(groupPlayerInfo, GroupList.G_PLAYER_INFO);
            _groupMaster.AddChildGroup(groupFlags, GroupList.G_FLAGS);

            groupInventoryMaster.AddChildGroup(groupInventoryPrimary, GroupList.G_INVENTORY_PRIMARY);
            groupInventoryMaster.AddChildGroup(groupInventoryEquipment, GroupList.G_INVENTORY_EQUIPMENT);
            groupInventoryMaster.AddChildGroup(groupInventoryFoodBag, GroupList.G_INVENTORY_FOOD_BAG);
            groupInventoryMaster.AddChildGroup(groupInventoryBombBag, GroupList.G_INVENTORY_BOMB_BAG);
            groupInventoryMaster.AddChildGroup(groupInventoryPendantBag, GroupList.G_INVENTORY_PENDANT_BAG);
            groupInventoryMaster.AddChildGroup(groupInventoryTreasureBag, GroupList.G_INVENTORY_TREASURE_BAG);

            _groupMaster.AddChildGroup(groupInventoryMaster, GroupList.G_INVENTORY);

            /// Define the skeleton cheats to base all the other codes off of
            // X-Position
            // Setup tree position is 944
            // v1.2.1
            Cheat xPosAddress = new Cheat("X-Pos (Ctrl + A/D to teleport)", 0x01364228, VarType.FLOAT);
            xPosAddress.AddOffset(0xB10);
            xPosAddress.AddOffset(0x18);
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
            // v1.2.1
            Cheat rupeesAddress = new Cheat("Rupees", 0x016B8838, VarType.DOUBLE);
            rupeesAddress.AddOffset(0x400); 
            rupeesAddress.AddOffset(0x210);

            // Has Pendant of Health
            // v1.2.1
            Cheat hasPendantOfHealth = new Cheat("HasPendantOfHealth", 0x016BC598, VarType.DOUBLE);
            hasPendantOfHealth.AddOffset(0x248);
            hasPendantOfHealth.AddOffset(0x90);
            hasPendantOfHealth.AddOffset(0xE80);

            // Has Master Key
            // v1.2.1
            Cheat hasMasterKey = new Cheat("HasMasterKey", 0x016BC5A0, VarType.DOUBLE);
            hasMasterKey.AddOffset(0x488);
            hasMasterKey.AddOffset(0x48);
            hasMasterKey.AddOffset(0x90);

            // Has Moon Pearl
            Cheat hasMoonPearl = new Cheat("HasMoonPearl", hasMasterKey, 0x20, VarType.DOUBLE);

            // Has Quiver
            Cheat hasQuiver = new Cheat("HasQuiver", hasMasterKey, -0xD0, VarType.DOUBLE);

            // Has Bomb Bag
            Cheat hasBombBag = new Cheat("HasBombBag", hasMasterKey, -0xE0, VarType.DOUBLE);

            // Has Treasure Bag
            Cheat hasTreasureBag = new Cheat("HasTreasureBag", hasMasterKey, -0x70, VarType.DOUBLE);

            // Has Pendant Bag
            Cheat hasPendantBag = new Cheat("HasPendantBag", hasMasterKey, 0x70, VarType.DOUBLE);

            // Has Food Bag
            Cheat hasFoodBag = new Cheat("HasFoodBag", hasMasterKey, 0x60, VarType.DOUBLE);

            // Bomb Bag Bomb Count
            // v1.2.1
            Cheat bombBagBombCount = new Cheat("Bomb Count (After Bomb Bag)", 0x016BC670, VarType.DOUBLE);
            bombBagBombCount.AddOffset(0x188);
            bombBagBombCount.AddOffset(0x200);

            // Arrow Count
            // v1.2.1
            Cheat arrowCount = new Cheat("Arrow Count", 0x016BC670, VarType.DOUBLE);
            arrowCount.AddOffset(0x188);
            arrowCount.AddOffset(0x108);
            arrowCount.AddOffset(0x0);

            // Inventory Slot 2 Item Quantity
            // v1.2.1
            Cheat slot2Quantity = new Cheat("Slot 2 Item Amount (Quantity)", 0x016BC670, VarType.DOUBLE);
            slot2Quantity.AddOffset(0x250);
            slot2Quantity.AddOffset(0x248);
            slot2Quantity.AddOffset(0x10);
            slot2Quantity.AddOffset(0x50);
            slot2Quantity.AddOffset(0x90);

            /// Generate all the other codes (and add the skeleton codes to the code list)

            // X/Y Position
            groupPlayerInfo.AddCheatEntry(xPosAddress, CheatList.X_POS);

            Cheat yPosAddress = new Cheat("Y-Pos (Ctrl + W/S to teleport)", xPosAddress, 0x4, VarType.FLOAT);
            yPosAddress.AddHotkey(HotkeyActions.DEC_VAL, new List<int>() { 17, 87 }, 32);
            yPosAddress.AddHotkey(HotkeyActions.INC_VAL, new List<int>() { 17, 83 }, 32);
            groupPlayerInfo.AddCheatEntry(yPosAddress, CheatList.Y_POS);

            // Player Status Codes
            Cheat playerMaxHealth = new Cheat("Max Health (Max: 16)", rupeesAddress, 0x180, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerMaxHealth, CheatList.MAX_HEALTH);
            Cheat playerCurrentHealth = new Cheat("Current Health (Max: 16)", playerMaxHealth, 0x10, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerCurrentHealth, CheatList.CUR_HEALTH);

            Cheat playerMagic = new Cheat("Magic (Max: 64)", rupeesAddress, 0xD0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerMagic, CheatList.MAGIC);
            groupPlayerInfo.AddCheatEntry(rupeesAddress, CheatList.RUPEES);
            //Cheat arrowCount = new Cheat("Arrow Count", rupeesAddress, -0x3BFA04B0, VarType.DOUBLE); // is also close to equippedtunic and ypos... does not seem to work unfortunately
            groupPlayerInfo.AddCheatEntry(arrowCount, CheatList.ARROW_COUNT);
            Cheat playerKeyCount = new Cheat("Key Count", playerMaxHealth, -0x20, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerKeyCount, CheatList.KEY_COUNT);

            groupPlayerInfo.AddCheatEntry(bombBagBombCount, CheatList.BOMB_BAG_BOMB_COUNT);

            //Cheat playerEquippedItemSlot = new Cheat("Equipped Item Slot (First slot is 7)", slot2Quantity, -0xBA0, VarType.DOUBLE);
            //groupPlayerInfo.AddCheatEntry(playerEquippedItemSlot, CheatList.EQUIPPED_ITEM_SLOT);

            Cheat playerEquippedTunic = new Cheat("Equipped Tunic (Max: 3)", rupeesAddress, -0xC0, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerEquippedTunic, CheatList.EQUIPPED_TUNIC);
            Cheat playerEquippedSword = new Cheat("Equipped Sword (Max: 5)", rupeesAddress, -0x80, VarType.FOUR_BYTE);
            groupPlayerInfo.AddCheatEntry(playerEquippedSword, CheatList.EQUIPPED_SWORD);
            Cheat playerEquippedShield = new Cheat("Equipped Shield (Max: 3)", rupeesAddress, -0x20, VarType.DOUBLE);
            groupPlayerInfo.AddCheatEntry(playerEquippedShield, CheatList.EQUIPPED_SHIELD);
            Cheat playerEquippedGlove = new Cheat("Equipped Glove (Max: 2)", rupeesAddress, 0x1C0, VarType.DOUBLE);
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

            groupFlags.AddCheatEntry(hasQuiver, CheatList.FLAG_QUIVER);
            groupFlags.AddCheatEntry(hasOilLamp, CheatList.FLAG_OIL_LAMP);

            // Inventory Navigation Flags
            Cheat enableFoodBagMovement = new Cheat("CanNavigateInventory (Food Bag)", slot2Quantity, 0x220 + (-0x80 * 2) + 0x310 + 0x7D0, VarType.DOUBLE);
            groupFlags.AddCheatEntry(enableFoodBagMovement, CheatList.FLAG_INVENTORY_NAVIGATION_FOOD);

            Cheat enableBombBagNavigation = new Cheat("CanNavigateInventory (Bomb Bag)", slot2Quantity, 0x220 + 0x870, VarType.DOUBLE);
            groupFlags.AddCheatEntry(enableBombBagNavigation, CheatList.FLAG_INVENTORY_NAVIGATION_BOMBS);

            Cheat enablePendantBagMovement = new Cheat("CanNavigateInventory (Pendant Bag)", slot2Quantity, 0x220 + (-0x80 * 1) + 0x680, VarType.DOUBLE);
            groupFlags.AddCheatEntry(enablePendantBagMovement, CheatList.FLAG_INVENTORY_NAVIGATION_PENDANTS);

            Cheat enableTreasureBagMovement = new Cheat("CanNavigateInventory (Treasure Bag)", slot2Quantity, 0x220 + (-0x80 * 2) + 0x310, VarType.DOUBLE);
            groupFlags.AddCheatEntry(enableTreasureBagMovement, CheatList.FLAG_INVENTORY_NAVIGATION_TREASURES);

            // Bag Owned Flags
            groupFlags.AddCheatEntry(hasFoodBag, CheatList.FLAG_FOOD_BAG);
            groupFlags.AddCheatEntry(hasBombBag, CheatList.FLAG_BOMB_BAG);
            groupFlags.AddCheatEntry(hasPendantBag, CheatList.FLAG_PENDANT_BAG);
            groupFlags.AddCheatEntry(hasTreasureBag, CheatList.FLAG_TREASURE_BAG);

            // Primary Inventory Slots
            InventorySlot[] primaryInventorySlots = new InventorySlot[TotalPrimaryInventorySlots];

            int slotOffsetCount = 1 + NegativeInventoryEntries;
            int offset = 0x80 * slotOffsetCount; // i = 0

            for (var i = 0; i < primaryInventorySlots.Length; i++)
            {
                primaryInventorySlots[i] = new InventorySlot(i + 1, slot2Quantity, offset);

                string usedString = string.Empty;
                if (i > UsedInventorySlots)
                    usedString = " (Unused)";
                CheatGroup thisGroupInvSlot = primaryInventorySlots[i].GenerateCheatGroup(inventoryCodesCounter, string.Format("Slot {0}" + usedString, i + 1));
                groupInventoryPrimary.AddChildGroup(thisGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
                inventoryCodesCounter++;

                offset -= 0x80;
            }
            inventoryGroupsCounter++;

            // Main Slot Item Class Codes for each of the categorized bags
            offset = 0x300;
            InventorySlot slotEquipmentBagMain = new InventorySlot(-1, slot2Quantity, offset);
            CheatGroup tmpGroupInvSlot = slotEquipmentBagMain.GenerateCheatGroup(inventoryCodesCounter, "Main Slot");
            groupInventoryEquipment.AddChildGroup(tmpGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
            inventoryCodesCounter++;
            inventoryGroupsCounter++;

            offset = 0x200;
            InventorySlot slotBombBagMain = new InventorySlot(-1, slot2Quantity, offset);
            tmpGroupInvSlot = slotBombBagMain.GenerateCheatGroup(inventoryCodesCounter, "Main Slot");
            groupInventoryBombBag.AddChildGroup(tmpGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
            inventoryCodesCounter++;
            inventoryGroupsCounter++;

            offset = 0x280;
            InventorySlot slotFoodBagMain = new InventorySlot(-1, slot2Quantity, offset);
            tmpGroupInvSlot = slotFoodBagMain.GenerateCheatGroup(inventoryCodesCounter, "Main Slot");
            groupInventoryFoodBag.AddChildGroup(tmpGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
            inventoryCodesCounter++;
            inventoryGroupsCounter++;

            offset = 0x180;
            InventorySlot slotPendantBagMain = new InventorySlot(-1, slot2Quantity, offset);
            tmpGroupInvSlot = slotPendantBagMain.GenerateCheatGroup(inventoryCodesCounter, "Main Slot");
            groupInventoryPendantBag.AddChildGroup(tmpGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
            inventoryCodesCounter++;
            inventoryGroupsCounter++;

            offset = 0x100;
            InventorySlot slotTreasureBagMain = new InventorySlot(-1, slot2Quantity, offset);
            tmpGroupInvSlot = slotTreasureBagMain.GenerateCheatGroup(inventoryCodesCounter, "Main Slot");
            groupInventoryTreasureBag.AddChildGroup(tmpGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
            inventoryCodesCounter++;
            inventoryGroupsCounter++;

            // Treasure Bag Inventory Slots
            InventorySlot[] treasureInventorySlots = new InventorySlot[TreasureInventorySlots];

            offset = 0x6F0;

            for (var i = 0; i < treasureInventorySlots.Length; i++)
            {
                treasureInventorySlots[i] = new InventorySlot(i + 1, slot2Quantity, offset);

                CheatGroup thisGroupInvSlot = treasureInventorySlots[i].GenerateCheatGroup(inventoryCodesCounter, string.Format("Slot {0}", i + 1));
                groupInventoryTreasureBag.AddChildGroup(thisGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
                inventoryCodesCounter++;

                offset -= 0x80;
            }
            inventoryGroupsCounter++;

            // Food Bag Inventory Slots
            InventorySlot[] foodInventorySlots = new InventorySlot[FoodInventorySlots];

            offset = 0xD40;

            for (var i = 0; i < foodInventorySlots.Length; i++)
            {
                foodInventorySlots[i] = new InventorySlot(i + 1, slot2Quantity, offset);

                CheatGroup thisGroupInvSlot = foodInventorySlots[i].GenerateCheatGroup(inventoryCodesCounter, string.Format("Slot {0}", i + 1));
                groupInventoryFoodBag.AddChildGroup(thisGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
                inventoryCodesCounter++;

                offset -= 0x80;
            }
            inventoryGroupsCounter++;

            // Bomb Bag Inventory Slot
            InventorySlot[] bombInventorySlots = new InventorySlot[BombInventorySlots];

            offset = 0xAD0;

            for (var i = 0; i < bombInventorySlots.Length; i++)
            {
                bombInventorySlots[i] = new InventorySlot(i + 1, slot2Quantity, offset);

                CheatGroup thisGroupInvSlot = bombInventorySlots[i].GenerateCheatGroup(inventoryCodesCounter, string.Format("Slot {0}", i + 1));
                groupInventoryBombBag.AddChildGroup(thisGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
                inventoryCodesCounter++;

                offset -= 0x80;
            }
            inventoryGroupsCounter++;

            // Pendant Bag Inventory Slot
            InventorySlot[] pendantInventorySlots = new InventorySlot[PendantInventorySlots];

            offset = 0x960;

            for (var i = 0; i < pendantInventorySlots.Length; i++)
            {
                pendantInventorySlots[i] = new InventorySlot(i + 1, slot2Quantity, offset);

                CheatGroup thisGroupInvSlot = pendantInventorySlots[i].GenerateCheatGroup(inventoryCodesCounter, string.Format("Slot {0}", i + 1));
                groupInventoryPendantBag.AddChildGroup(thisGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
                inventoryCodesCounter++;

                offset -= 0x80;
            }
            inventoryGroupsCounter++;

            // Equipment Inventory Slot
            InventorySlot[] equipmentInventorySlots = new InventorySlot[EquipmentInventorySlots];

            offset = 0x1130;

            for (var i = 0; i < equipmentInventorySlots.Length; i++)
            {
                equipmentInventorySlots[i] = new InventorySlot(i + 1, slot2Quantity, offset);

                CheatGroup thisGroupInvSlot = equipmentInventorySlots[i].GenerateCheatGroup(inventoryCodesCounter, string.Format("Slot {0}", i + 1));
                groupInventoryEquipment.AddChildGroup(thisGroupInvSlot, GroupList.G_INV_SLOTS_BEGIN + inventoryGroupsCounter);
                inventoryCodesCounter++;

                offset -= 0x80;
            }
            inventoryGroupsCounter++;

        }

        public static int GetXmlID()
        {
            return XmlID;
        }

        public static void IncrementXmlID()
        {
            XmlID++;
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
            return GroupCount;
        }

        public static void IncrementGroupCount()
        {
            GroupCount++;
        }
    }
}