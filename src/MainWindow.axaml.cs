using Avalonia.Controls;
using Avalonia.Interactivity;
using DungeonsOfInfinityTrainer.CheatManagement;
using Memory;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DungeonsOfInfinityTrainer
{
    public partial class MainWindow : Window
    {

        /*
        Pendants are 10 bytes apart, starting at pendant of health


        ------------------------------------------------------Double to 4byte conversion------------------------------------------------
        if the variable is stored as a double, its 4byte counterpart will be 4 bytes larger then the orginal address, these values below
                                                            are the converted values
        ---------------------------------------------------------------------------------------------------------------------------------

        Double|4Byte

            -1|3220176896
             0|0
             1|1072693248
             2|1073741824
             3|1074266112
             4|1074790400
             5|1075052544
             6|1075314688
             7|1075576832
             8|1075838976
             9|1075970048
            10|1076101120
            11|1076232192
            12|1076363264
            13|1076494336
            14|1076625408
            15|1076756480
            16|1076887552
        */

        public const string GameVersion = "1.1.4";
        public const string CheatVersion = "1.0";
        public const string ProcessName = "Dungeons of Infinity";
        public long BaseOffset = 0;
        public long AddrBaseOffset = 0;

        private static readonly CheatManager s_CheatManager = new CheatManager();
        private static readonly CheatGroup s_groupMaster = s_CheatManager.GetCheatTree();
        private static readonly Mem s_mem = new Mem();
        private static Cheat? s_currentCheat;

        private static bool s_processOpen = false;


        public MainWindow()
        {
            InitializeComponent();

            // Open the game process
            s_processOpen = s_mem.OpenProcess(ProcessName);
            Thread.Sleep(300);

            // Test Cheat Modification
            if (s_processOpen) {
                ProcessCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.MAGIC, "64", "Write");
            }
            
            // Attach the event handler for the button click
            CheatExportButton.Click += ClickHandler;
        }

        private async void ClickHandler(object? sender, RoutedEventArgs e)
        {
            CheatGroup groupMaster = s_CheatManager.GetCheatTree();
            List<string> output = s_CheatManager.EmitAllCheats(groupMaster);
            string fileName = string.Format("[v{0}] The Legend of Zelda - Dungeons of Infinity (v{1}).CT", GameVersion, CheatVersion);
            StreamWriter file = new StreamWriter(fileName);
            foreach (string s in output)
            {
                file.WriteLine(s);
            }
            file.Flush();
            file.Close();

            // Create a new instance of CheatExportDonePrompt
            var cheatExportDonePrompt = new CheatExportDonePrompt();

            // Show the window as a dialog (modal window)
            await cheatExportDonePrompt.ShowDialog(this);

            // Update the TextBlock with a new message
            message.Text = string.Format("Exported .CT to {0}!", fileName);
        }

        private string ProcessCheat(CheatManager.GroupList groupList, CheatManager.CheatList chosenCheat, string desiredValue, string desiredResult)
        {
            CheatGroup GroupSub = s_groupMaster.GetChildGroup(groupList);
            s_currentCheat = GroupSub.GetCheatEntry(chosenCheat)[0];

            BaseOffset = s_currentCheat.GetBaseAddress();
            List<int> tmpOffsets = s_currentCheat.GetAddressOffsets();
            VarType tmpType = s_currentCheat.GetVarType();
            string tmpTypeStr = string.Empty;

            string tmpAddrStr = "base+";
            tmpAddrStr += string.Format("{0:X1}", BaseOffset);
            foreach (int o in tmpOffsets)
            {
                tmpAddrStr += string.Format(",{0:X1}", o);
            }

            switch (tmpType)
            {
                case VarType.FOUR_BYTE:
                    tmpTypeStr = "fourbyte";
                    break;
                default:
                case VarType.DOUBLE:
                    tmpTypeStr = "double";
                    break;
                case VarType.FLOAT:
                    tmpTypeStr = "float";
                    break;
            }
            if (desiredResult == "Write")
            {
                s_mem.WriteMemory(tmpAddrStr, tmpTypeStr, desiredValue);
                return null;
            }
            else if (desiredResult == "Read")
            {
                switch (tmpType)
                {
                    case VarType.FOUR_BYTE:
                        return s_mem.ReadInt(tmpAddrStr).ToString();
                    case VarType.DOUBLE:
                        return s_mem.ReadDouble(tmpAddrStr).ToString();
                    case VarType.FLOAT:
                        return s_mem.ReadFloat(tmpAddrStr).ToString();
                    default:
                        return "N/A";
                }

            }
            else
                return null;
        }
    }
}