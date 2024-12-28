using System.Collections.Generic;

namespace DungeonsOfInfinityTrainer.CheatManagement
{


    public class CheatGroup
    {
        private Dictionary<CheatManager.CheatList, Cheat> _cheatLookup = new Dictionary<CheatManager.CheatList, Cheat>();
        private readonly int _xmlID = 0;
        List<Cheat> _cheatList;
        private string _groupDescription;
        private int _groupID = 1;
        private readonly List<CheatGroup> _childGroups = new List<CheatGroup>();
        private CheatManager.GroupList _groupList;

        public CheatGroup(string groupDescription, bool isChildGroup = false)
        {
            _groupDescription = groupDescription;
            _groupID = CheatManager.GetGroupCount();

            if (!isChildGroup)
                CheatManager.IncrementGroupCount();

            _xmlID = CheatManager.GetXmlID();
            CheatManager.IncrementXmlID();
            _cheatList = new List<Cheat>();
        }

        internal void AddCheatEntry(Cheat cheat, CheatManager.CheatList cheatEntry)
        {
            _cheatList.Add(cheat);
            if (cheatEntry == CheatManager.CheatList.UNDEFINED)
                return;
            _cheatLookup.Add(cheatEntry, cheat);
        }

        public List<Cheat> GetCheatEntry(CheatManager.CheatList cheatEntry)
        {
            List<Cheat> returnedCheats = new List<Cheat>();
            if (cheatEntry >= CheatManager.CheatList.INV_SLOTS_BEGIN)
            {
                returnedCheats.Add(_cheatLookup[cheatEntry]);
                int chIndex = _cheatList.IndexOf(_cheatLookup[cheatEntry]);
                returnedCheats.Add(_cheatList[chIndex + 1]);
                returnedCheats.Add(_cheatList[chIndex + 2]);
                returnedCheats.Add(_cheatList[chIndex + 3]);
                returnedCheats.Add(_cheatList[chIndex + 4]);
            }
            else
            {
                returnedCheats.Add(_cheatLookup[cheatEntry]);
            }
            return returnedCheats;
        }

        internal void AddChildGroup(CheatGroup cheatGroup, CheatManager.GroupList groupList)
        {
            cheatGroup.SetParentGroupID((int)_groupList);
            cheatGroup.SetGroupList(groupList);
            _childGroups.Add(cheatGroup);
        }

        private void SetGroupList(CheatManager.GroupList groupList)
        {
            _groupList = groupList;
        }

        internal List<CheatGroup> GetChildGroups()
        {
            return _childGroups;
        }

        internal CheatGroup GetChildGroup(CheatManager.GroupList groupList)
        {
            foreach (var cheatGroup in _childGroups)
            {
                if (cheatGroup.GetGroupList() == groupList)
                {
                    return cheatGroup;
                }
            }
            return null;
        }

        private CheatManager.GroupList GetGroupList()
        {
            return _groupList;
        }

        private void SetParentGroupID(int groupID)
        {
            _groupID = groupID;
        }

        internal void Emit(List<string> output)
        {
            output.Add("<CheatEntry>");
            output.Add(string.Format("<ID>{0}</ID>", _xmlID));
            output.Add(string.Format("<Description>\"{0}\"</Description>", _groupDescription));
            output.Add("<Options moHideChildren=\"1\"/>");
            output.Add(string.Format("<GroupHeader>1</GroupHeader>"));

            if (_childGroups.Count > 0 || _cheatList.Count > 0)
            {
                output.Add("<CheatEntries>");

                if (_childGroups.Count > 0)
                {
                    foreach (CheatGroup cg in _childGroups)
                    {
                        cg.Emit(output);
                    }
                }

                if (_cheatList.Count > 0)
                {
                    foreach (Cheat c in _cheatList)
                    {
                        c.EmitCheat(output);
                    }
                }

                output.Add("</CheatEntries>");
            }
            output.Add("</CheatEntry>");
        }

        List<string> EmitGroup()
        {
            List<string> groupOut = new List<string>();

            return groupOut;
        }
    }
}