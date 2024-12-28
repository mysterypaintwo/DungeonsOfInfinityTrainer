using static DungeonsOfInfinityTrainer.CheatManagement.CheatManager;

namespace DungeonsOfInfinityTrainer.CheatManagement
{
    internal class InventorySlot
    {
        private Cheat _slotQuantity;
        private Cheat _slotEnabled;
        private Cheat _slotIDDoub;
        private Cheat _slotIDFourByte;
        private Cheat _slotSecIDDoub;
        private Cheat _slotSecIDFourByte;
        private Cheat _slotSecIDFloat;

        public InventorySlot(int id, Cheat baseCheat, long addressOffset)
        {
            string slotName = string.Format("Slot {0}", id);
            if (id < 0)
            {
                slotName = "Main Slot";
            }
            _slotEnabled = new Cheat(slotName + " Disabled", baseCheat, (int)(addressOffset - 0x20), VarType.DOUBLE);
            _slotQuantity = new Cheat(slotName + " Quantity", baseCheat, (int)(addressOffset), VarType.DOUBLE);
            _slotIDDoub = new Cheat(slotName + " Item Class (ID, Double [Adds to Inventory])", baseCheat, (int)(addressOffset + 0x20), VarType.DOUBLE);
            _slotIDDoub.EnableInvDropdownList();
            _slotIDFourByte = new Cheat(slotName + " Item Class (ID, 4 Byte [Modifies Inventory])", baseCheat, (int)(addressOffset + 0x20), VarType.FOUR_BYTE);
            _slotIDFourByte.EnableInvDropdownList();
            _slotSecIDDoub = new Cheat(slotName + " Item Index (Secondary ID, Double)", baseCheat, (int)(addressOffset + 0x10), VarType.DOUBLE);
            _slotSecIDFourByte = new Cheat(slotName + " Item Index (Secondary ID, 4 Byte)", baseCheat, (int)(addressOffset + 0x10), VarType.FOUR_BYTE);
            _slotSecIDFloat = new Cheat(slotName + " Item Index (Secondary ID, Float)", baseCheat, (int)(addressOffset + 0x10), VarType.FLOAT);
        }

        internal CheatGroup GenerateCheatGroup(int inventoryCodesCounter, string groupName)
        {
            CheatGroup cg = new CheatGroup(groupName);

            cg.AddCheatEntry(_slotEnabled, CheatList.UNDEFINED);
            cg.AddCheatEntry(_slotQuantity, CheatList.INV_SLOTS_BEGIN + inventoryCodesCounter);
            cg.AddCheatEntry(_slotIDDoub, CheatList.UNDEFINED);
            cg.AddCheatEntry(_slotIDFourByte, CheatList.UNDEFINED);
            cg.AddCheatEntry(_slotSecIDDoub, CheatList.UNDEFINED);
            cg.AddCheatEntry(_slotSecIDFourByte, CheatList.UNDEFINED);
            cg.AddCheatEntry(_slotSecIDFloat, CheatList.UNDEFINED);

            return cg;
        }
    }
}