using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Memory;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Xml.Linq;
using System.Linq.Expressions;



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
namespace Dungeons_Of_Infinity_Trainer
{

    //
    public partial class MainForm : Form
    {
        //Initializing Vars
        //Version Variables
        public static string GameVersion = "1.1.3";
        public static string CheatVersion = "0.0.2";

        //Misc Vars
        public Point mouseLocation;
        public Mem m = new Mem();
        public Process processHandel = null;
        public bool processOpen = false;
        public bool Edit = false;

        //Proccess Variables
        public string pName = "Dungeons of Infinity";


        public static CheatManager CheatManager = null;

        //-----------ADDRESSES-----------//

        //Cheat Groups
        CheatGroup GroupMaster;
        CheatGroup Group_Sub;
        CheatGroup AddrSubGroup;

        //Cheats
        public Cheat currentCheat;
        public Cheat addrCheat;


        public long baseOffset = 0;
        public long addrBaseOffset = 0;
        public long TEMP_MEM = 0;

        //-----------CHEATFLAGS----------//
        bool InfinitePlayerStatsFlag = false;

        public MainForm()
        {
            InitializeComponent();
            CheatManager = new CheatManager();
            GroupMaster = CheatManager.GetCheatGroup();
            processLabel.Text = processOpen.ToString();
        }


        private void ChangeLabel(System.Windows.Forms.TextBox textBoxName, string text)
        {
            textBoxName.Text = text;
        }
        private void ChangeLabel(Label labelName, string text)
        {
            labelName.Text = text;
        }

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                processOpen = m.OpenProcess(pName);
                Thread.Sleep(300);
                BGWorker.ReportProgress(0);

            }
        }

        private string GetCheat(CheatManager.GroupList groupList, CheatManager.CheatList chosenCheat, string desiredValue, string desiredResult)
        {
            Group_Sub = GroupMaster.GetChildGroup(groupList);
            currentCheat = Group_Sub.GetCheatEntry(chosenCheat)[0];

            baseOffset = currentCheat.GetBaseAddress();
            List<int> tmpOffsets = currentCheat.GetAddressOffsets();
            VarType tmpType = currentCheat.GetVarType();
            string tmpTypeStr = string.Empty;

            string tmpAddrStr = "base+";
            tmpAddrStr += String.Format("{0:X1}", baseOffset);
            foreach (int o in tmpOffsets)
            {
                tmpAddrStr += String.Format(",{0:X1}", o);
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
                m.WriteMemory(tmpAddrStr, tmpTypeStr, desiredValue);
                return null;
            }
            else if (desiredResult == "Read")
            {
                switch (tmpType)
                {
                    case VarType.FOUR_BYTE:
                        return m.ReadInt(tmpAddrStr).ToString();
                    case VarType.DOUBLE:
                        return m.ReadDouble(tmpAddrStr).ToString();
                    case VarType.FLOAT:
                        return m.ReadFloat(tmpAddrStr).ToString();
                    default:
                        return "N/A";
                }

            }
            else
                return null;
        }

        private void Activate_Button_Click(object sender, EventArgs e)
        {
            if (processOpen)
            {
                //GetCheat();
            }
            else
                MessageBox.Show("No Process Detected");
        }

        private void BGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!processOpen)
            {
                ChangeLabel(processLabel, "Game Not Found");
                return;
            }
            ChangeLabel(processLabel, "Game Found");
            ChangeLabel(XPosDisplay, GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.X_POS, "", "Read"));
            ChangeLabel(YPosDisplay, GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.Y_POS, "", "Read"));
            if (!Edit)
            {
                ChangeLabel(MaxHealthDisplay, GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.MAX_HEALTH, "", "Read"));
                ChangeLabel(CurrentHealthDisplay, GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.CUR_HEALTH, "", "Read"));
                ChangeLabel(MagicDisplay, GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.MAGIC, "", "Read"));
                ChangeLabel(RupeesDisplay, GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.RUPEES, "", "Read"));
            }
            if (InfinitePlayerStatsFlag)
            {
                GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.MAX_HEALTH, "16", "Write");
                GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.CUR_HEALTH, "16", "Write");
                GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.MAGIC, "64", "Write");
                GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.RUPEES, "9999", "Write");

            }

        }

        private void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }
        private void Export_Click(object sender, EventArgs e)
        {
            CheatGroup groupMaster = CheatManager.GetCheatGroup();
            List<string> output = CheatManager.EmitAllCheats(groupMaster);
            string fileName = string.Format("[v{0}] The Legend of Zelda - Dungeons of Infinity (v{1}).CT", GameVersion, CheatVersion);
            StreamWriter file = new StreamWriter(fileName);
            foreach (string s in output)
            {
                file.WriteLine(s);
            }
            file.Flush();
            file.Close();
            MessageBox.Show("Export Successful");
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void CurrentHealthConfirm(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (!processOpen)
                {
                    MessageBox.Show("No Process Detected");
                    return;
                }
                int x;
                try
                {
                    x = Int32.Parse(MaxHealthDisplay.Text);
                }
                catch (Exception c)
                {
                    MessageBox.Show("Invalid Parameter");
                    return;
                }

                //MessageBox.Show(CurrentHealthDisplay.Text);
                GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.CUR_HEALTH, CurrentHealthDisplay.Text, "Write");
                this.ActiveControl = null;
                e.Handled = true;
                Edit = false;
            }
        }

        private void MaxHealthConfirm(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (!processOpen)
                {
                    MessageBox.Show("No Process Detected");
                    return;
                }
                int x;
                try
                {
                    x = Int32.Parse(MaxHealthDisplay.Text);
                }
                catch (Exception c)
                {
                    MessageBox.Show("Invalid Parameter");
                    return;
                }

                //MessageBox.Show(CurrentHealthDisplay.Text);
                GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.MAX_HEALTH, MaxHealthDisplay.Text, "Write");
                this.ActiveControl = null;
                e.Handled = true;
                Edit = false;
            }
        }

        private void InfiniteHealthCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (!processOpen)
            {
                InfiniteHealthCheck.Checked = false;
                MessageBox.Show("No Process Detected");
                return;
            }
            InfinitePlayerStatsFlag = !InfinitePlayerStatsFlag;
        }

        private void Editing(object sender, EventArgs e)
        {
            Edit = true;
        }

        private void DoneEditing(object sender, EventArgs e)
        {
            Edit = false;
        }

        private void MagicConfirm(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!processOpen)
                {
                    MessageBox.Show("No Process Detected");
                    return;
                }
                int x;
                try
                {
                    x = Int32.Parse(MaxHealthDisplay.Text);
                }
                catch (Exception c)
                {
                    MessageBox.Show("Invalid Parameter");
                    return;
                }

                //MessageBox.Show(CurrentHealthDisplay.Text);
                GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.MAGIC, MagicDisplay.Text, "Write");
                this.ActiveControl = null;
                e.Handled = true;
                e.SuppressKeyPress = true;
                Edit = false;
            }

        }

        private void RupeesConfirm(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!processOpen)
                {
                    MessageBox.Show("No Process Detected");
                    return;
                }
                int x;
                try
                {
                    x = Int32.Parse(RupeesDisplay.Text);
                }
                catch (Exception c)
                {
                    MessageBox.Show("Invalid Parameter");
                    return;
                }

                GetCheat(CheatManager.GroupList.G_PLAYER_INFO, CheatManager.CheatList.RUPEES, RupeesDisplay.Text, "Write");
                this.ActiveControl = null;
                e.Handled = true;
                e.SuppressKeyPress = true;
                Edit = false;
            }

        }
    }
}
