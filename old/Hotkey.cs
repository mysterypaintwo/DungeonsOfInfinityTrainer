using System.Collections.Generic;

namespace Dungeons_Of_Infinity_Trainer
{
    internal class Hotkey
    {
        private CheatManager.HotkeyActions _hkAction;
        private List<int> _keystrokeList;
        private int _value;

        public Hotkey(CheatManager.HotkeyActions hkAction, List<int> keystrokeList, int value)
        {
            _hkAction = hkAction;
            _keystrokeList = keystrokeList;
            _value = value;
        }

        public CheatManager.HotkeyActions GetHotkeyAction()
        {
            return _hkAction;
        }

        public List<int> GetKeystrokeList()
        {
            return _keystrokeList;
        }

        public int GetValue()
        {
            return _value;
        }
    }
}