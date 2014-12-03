using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPMCodeGen
{
    public partial class historyWin : Form
    {
        public int idInt = 0;
        public historyWin()
        {
            InitializeComponent();
            codes.BPMGen bpm = new codes.BPMGen();
            this.dataGridView1.DataSource = bpm.GetList("1=1 order by createDat desc").Tables[0];
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                idInt = int.Parse(dataGridView1.SelectedRows[0].Cells["IDInt"].Value.ToString());
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            codes.BPMGen bpm = new codes.BPMGen();
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult drr = MessageBox.Show("确定要删除吗？","删除",MessageBoxButtons.OKCancel);
                if(drr==DialogResult.OK)
                {
                    foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                    {
                        if (dr.IsNewRow)
                        {
                            continue;
                        }
                        bpm.Delete(int.Parse(dataGridView1.SelectedRows[0].Cells["IDInt"].Value.ToString()));
                    }
                }
            }
            
            this.dataGridView1.DataSource = bpm.GetList("1=1 order by createDat desc").Tables[0];
        }

    }
}
