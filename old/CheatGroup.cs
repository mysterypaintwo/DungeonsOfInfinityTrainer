using System.Collections.Generic;
using System;
using System.Drawing.Text;
using System.Linq;

namespace Dungeons_Of_Infinity_Trainer
{


    public class CheatGroup
    {
        private Dictionary<CheatManager.CheatList, Cheat> _cheatDict = new Dictionary<CheatManager.CheatList, Cheat>();
        private int _xmlID = 0;
        List<Cheat> _cheatEntries;
        private string _groupDescription;
        private int _groupID = 1;
        private List<CheatGroup> _childGroups = new List<CheatGroup>();
        private CheatManager.CheatList _cheatEntry;
        private CheatManager.GroupList _groupList;

        public CheatGroup(string groupDescription, bool isChildGroup = false)
        {
            _groupDescription = groupDescription;
            _groupID = CheatManager.GroupCount;

            if (!isChildGroup)
                CheatManager.GroupCount++;

            _xmlID = CheatManager.XmlID;
            CheatManager.XmlID++;
            _cheatEntries = new List<Cheat>();
        }

        internal void AddCheatEntry(Cheat cheat, CheatManager.CheatList cheatEntry)
        {
            _cheatEntries.Add(cheat);
            if (cheatEntry == CheatManager.CheatList.UNDEFINED)
                return;
            _cheatDict.Add(cheatEntry,cheat);
        }

        public List<Cheat> GetCheatEntry(CheatManager.CheatList cheatEntry)
        {
            List<Cheat> returnedCheats = new List<Cheat>();
            if (cheatEntry >= CheatManager.CheatList.INV_SLOT_0 && cheatEntry <= CheatManager.CheatList.INV_SLOT_17)
            {
                returnedCheats.Add(_cheatDict[cheatEntry]);
                int chIndex = _cheatEntries.IndexOf(_cheatDict[cheatEntry]);
                returnedCheats.Add(_cheatEntries[chIndex + 1]);
                returnedCheats.Add(_cheatEntries[chIndex + 2]);
                returnedCheats.Add(_cheatEntries[chIndex + 3]);
                returnedCheats.Add(_cheatEntries[chIndex + 4]);
            }
            else
            {
                returnedCheats.Add(_cheatDict[cheatEntry]);
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
            output.Add(String.Format("<ID>{0}</ID>", _xmlID));
            output.Add(String.Format("<Description>\"{0}\"</Description>", _groupDescription));
            output.Add("<Options moHideChildren=\"1\"/>");
            output.Add(String.Format("<GroupHeader>1</GroupHeader>"));

            if (_childGroups.Count > 0 || _cheatEntries.Count > 0)
            {
                output.Add("<CheatEntries>");

                if (_childGroups.Count > 0)
                {
                    foreach (CheatGroup cg in _childGroups)
                    {
                        cg.Emit(output);
                    }
                }

                if (_cheatEntries.Count > 0)
                {
                    foreach (Cheat c in _cheatEntries)
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