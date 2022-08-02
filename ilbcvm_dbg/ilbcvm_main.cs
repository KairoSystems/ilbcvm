using ilbcvm;
using ilbcvm_cc;
using ilbcvm_RT;

namespace ilbcvm_dbg
{
    public partial class ilbcvm_dbg : Form
    {
        byte[] prog;

        public static VM machine;

        public ilbcvm_dbg()
        {
            InitializeComponent();
            prog = Array.Empty<byte>();
        }

        public static byte[] HelloWorld =
        {
            065, 245, 002, 000, 000, 000, 065, 253, 250, 000,
            000, 000, 250, 069, 250, 000, 000, 000, 250, 110,
            251, 000, 000, 000, 250, 116, 252, 000, 000, 000,
            250, 101, 253, 000, 000, 000, 250, 114, 254, 000,
            000, 000, 250, 255, 000, 000, 000, 250, 089, 000,
            001, 000, 000, 250, 111, 001, 001, 000, 000, 250,
            117, 002, 001, 000, 000, 250, 114, 003, 001, 000,
            000, 250, 004, 001, 000, 000, 250, 078, 005, 001,
            000, 000, 250, 097, 006, 001, 000, 000, 250, 109,
            007, 001, 000, 000, 250, 101, 008, 001, 000, 000,
            250, 058, 001, 000, 000, 250, 001, 000, 000, 065,
            247, 017, 000, 000, 000, 235, 001, 000, 000, 000,
            000, 065, 245, 004, 000, 000, 000, 065, 253, 244,
            001, 000, 000, 235, 001, 000, 000, 000, 000, 250,
            072, 237, 001, 000, 000, 250, 101, 238, 001, 000,
            000, 250, 108, 239, 001, 000, 000, 250, 108, 240,
            001, 000, 000, 250, 111, 241, 001, 000, 000, 250,
            044, 242, 001, 000, 000, 250, 243, 001, 000, 000,
            065, 253, 237, 001, 000, 000, 068, 247, 007, 000,
            000, 000, 065, 245, 002, 000, 000, 000, 235, 001,
            000, 000, 000, 000, 065, 245, 001, 000, 000, 000,
            065, 246, 000, 000, 000, 235, 001, 000, 000, 000,
            000, 208, 000, 000, 000, 000, 000, 000, 000, 000
        };

        private void button1_Click(object sender, EventArgs e)
        {
            string path = null;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                ofd.Filter = "ilbcvm Binary |*.ivb";
                ofd.FilterIndex = 2;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (ofd.FileName != "")
                    {
                        path = ofd.FileName;
                        prog = File.ReadAllBytes(path);
                        StatusLbl.Text = "File loaded: " + Path.GetFileName(path);
                        decomp_bin(prog);
                        btnStart.Enabled = true;
                        btnTick.Enabled = true;
                        btnStart.Text = "Start";
                    }
                }
                else
                {
                    // Load 'Hello World' example
                    prog = HelloWorld;
                    StatusLbl.Text = "File loaded: Hello World Example";
                    decomp_bin(prog);
                    btnStart.Enabled = true;
                    btnTick.Enabled = true;
                    btnStart.Text = "Start";
                }
            }
            machine = new VM(prog, prog.Length + 2048);
            ilbcvm.Globals.RuntimeOptions.console = new AR_Console();
        }

        private void decomp_bin(byte[] prg)
        {
            Decompiler Decompiler = new Decompiler(prg);
            string SourceCode = Decompiler.Decompile();
            string[] ops = SourceCode.Split("\n");
            foreach (string str in ops)
            {
                listBox1.Items.Add(str);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                btnStart.Text = "Stop";
                // VM hasn't been started/initialized yet
                btnStart.Text = "Start";
                ilbcvm.Globals.RuntimeOptions.console = new AR_Console();
                machine.Execute();
                while (machine.Running == true)
                {
                    StatusLblRunning.Text = "Running: true";
                    machine.Tick();
                    UpdateRegs();
                }
                machine.Halt();
            }
            else if (btnStart.Text == "Stop")
            {
                btnStart.Text = "Start";
            }
            
        }


        private void btnTick_Click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex++;
            machine.Tick();
            UpdateRegs();
            
        }

        private void UpdateRegs()
        {
            txtPC.Text = machine.PC.ToString();
            txtIP.Text = machine.IP.ToString();
            txtSP.Text = machine.SP.ToString();
            txtSS.Text = machine.SS.ToString();
            txtAL.Text = machine.AL.ToString();
            txtAH.Text = machine.AH.ToString();
            txtBL.Text = machine.BL.ToString();
            txtBH.Text = machine.BH.ToString();
            txtCL.Text = machine.CL.ToString();
            txtCH.Text = machine.CH.ToString();
            txtX.Text = machine.X.ToString();
            txtY.Text = machine.Y.ToString();
        }
    }
}