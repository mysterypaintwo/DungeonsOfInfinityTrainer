﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_Of_Infinity_Trainer
{
    public enum VarType
    {
        UNDEFINED,
        BINARY,
        BYTE,
        TWO_BYTE,
        FOUR_BYTE,
        EIGHT_BYTE,
        FLOAT,
        DOUBLE,
        STRING,
        BYTE_ARRAY,
        MAX
    };

    public class Cheat
    {
        public static string InvDropDown = @"0:Quiver
1:Arrows
2:Bag
3:Book
4:BombBag
5:Bombs
6:Red Boomberang
7:Bow
8:Staff(green:0 blue:1 red:2)
9:Cape
10:Compass
11:Crystals
12:Fairy
13:Food
14:Gems
15:GemBag
16:Gloves
17:HeartPickup
18:HeartContainer
19:Hookshot
20:SmallKey
21:MasterKey
22:KingsKey
23:Kinstone
24:Lantern
25:Pazo'sLetter
26:SmallMagicPickup
27:HalfMagic
28:Map
29:Bombos
30:Mirror
31:MoonPearl
32:Mushroom
33:Orbs
34:Pendants
35:CocaCola
36:Bottles
37:MagicPowder
38:Rings
39:Scepters
40:RupeePickup
41:Shields
42:StarMan
43:StopWatch
44:Sword
45:Chest(DontOpen Crash)
46:Tunic
47:WishingStone";

        List<int> _addressOffsets;
        private long _baseAddress;
        private int _xmlID = 0;
        private string _cheatDescription;
        private VarType _varType = VarType.UNDEFINED;
        private bool _dropdownCheat = false;
        private List<Hotkey> _hotkeys = new List<Hotkey>();

        public Cheat(string cheatDescription, Cheat parentCheat, int shiftedOffsetAmount, VarType varType)
        {
            _cheatDescription = cheatDescription;
            _xmlID = CheatManager.XmlID;
            CheatManager.XmlID++;

            // Clone the values of the other cheat first

            _varType = varType;
            _baseAddress = parentCheat.GetBaseAddress();
            _addressOffsets = new List<int>();

            List<int> parentAddressOffsets = parentCheat.GetAddressOffsets();

            for (int i = 0; i < parentAddressOffsets.Count; i++)
            {
                _addressOffsets.Add(parentAddressOffsets[i]);
            }

            _addressOffsets[_addressOffsets.Count - 1] += shiftedOffsetAmount;
        }

        public List<int> GetAddressOffsets()
        {
            return _addressOffsets;
        }

        public long GetBaseAddress()
        {
            return _baseAddress;
        }

        public VarType GetVarType()
        {
            return _varType;
        }

        public void EnableInvDropdownList()
        {
            _dropdownCheat = true;
        }

        public Cheat(string cheatDescription, long baseAddress, VarType varType)
        {
            _addressOffsets = new List<int>();
            _baseAddress = baseAddress;
            _xmlID = CheatManager.XmlID;
            CheatManager.XmlID++;
            _cheatDescription = cheatDescription;
            _varType = varType;
        }

        internal void AddOffset(int offsetValue)
        {
            _addressOffsets.Add(offsetValue);
        }

        internal void EmitCheat(List<string> output)
        {
            output.Add("<CheatEntry>");
            output.Add(String.Format("<ID>{0}</ID>", _xmlID));
            output.Add(String.Format("<Description>\"{0}\"</Description>", _cheatDescription));

            if (_dropdownCheat)
            {
                output.Add("<DropDownList ReadOnly=\"1\">");
                output.Add(InvDropDown);
                output.Add("</DropDownList>");
            }
            string varType = string.Empty;

            switch (_varType)
            {
                case VarType.BINARY:
                    varType = "Binary";
                    break;
                case VarType.BYTE:
                    varType = "Byte";
                    break;
                case VarType.TWO_BYTE:
                    varType = "2 Bytes";
                    break;
                case VarType.FOUR_BYTE:
                    varType = "4 Bytes";
                    break;
                case VarType.EIGHT_BYTE:
                    varType = "8 Bytes";
                    break;
                case VarType.FLOAT:
                    varType = "Float";
                    break;
                case VarType.DOUBLE:
                    varType = "Double";
                    break;
                case VarType.STRING:
                    varType = "String";
                    break;
                case VarType.BYTE_ARRAY:
                    varType = "Array of byte";
                    break;
            }
            output.Add(String.Format("<VariableType>{0}</VariableType>", varType));

            output.Add(String.Format("<Address>\"{0}\"+{1:X0}</Address>", CheatManager.ProcessName, _baseAddress));

            if (_addressOffsets.Count > 0)
            {
                output.Add("<Offsets>");
                for (var _addressOffsetIndex = _addressOffsets.Count - 1; _addressOffsetIndex >= 0; _addressOffsetIndex--)
                {
                    output.Add(String.Format("<Offset>{0:X0}</Offset>", _addressOffsets[_addressOffsetIndex]));
                }
                output.Add("</Offsets>");
            }

            if (_hotkeys.Count > 0)
            {
                int hkID = 0;
                output.Add("<Hotkeys>");
                foreach(var hk in _hotkeys)
                {
                    output.Add("<Hotkey>");

                    string hkAction = String.Empty;

                    switch(hk.GetHotkeyAction())
                    {
                        default:
                        case CheatManager.HotkeyActions.DEC_VAL:
                            hkAction = "Decrease Value";
                            break;
                        case CheatManager.HotkeyActions.INC_VAL:
                            hkAction = "Increase Value";
                            break;
                    }

                    output.Add(String.Format("<Action>{0}</Action>", hkAction));

                    List<int> hkKeystrokes = hk.GetKeystrokeList();

                    if (hkKeystrokes.Count > 0)
                    {
                        output.Add("<Keys>");
                        foreach(int ks in hkKeystrokes)
                        {
                            output.Add(String.Format("<Key>{0}</Key>", ks));
                        }
                        output.Add("</Keys>");
                    }
                    output.Add(String.Format("<Value>{0}</Value>", hk.GetValue()));
                    output.Add(String.Format("<ID>{0}</ID>", hkID));

                    output.Add("</Hotkey>");
                    hkID++;
                }
                output.Add("</Hotkeys>");
            }

            output.Add("</CheatEntry>");
        }

        internal void AddHotkey(CheatManager.HotkeyActions hkAction, List<int> keystrokeList, int changedValue)
        {
            Hotkey hk = new Hotkey(hkAction, keystrokeList, changedValue);
            _hotkeys.Add(hk);
        }
    }
}
