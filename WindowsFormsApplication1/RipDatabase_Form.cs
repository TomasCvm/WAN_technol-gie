using System;
using System.Windows.Forms;

namespace xcicman_zadanie__WAN
{
    public partial class RipDatabaseForm : Form
    {
        StaticRouting _st = StaticRouting.Instance;
        public RipDatabaseForm()
        {
            InitializeComponent();
            ripDatabaseGrid.DataSource = RipDatabaseTable.fill_ripDatabase_table(_st.RipDatabase);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ripDatabaseGrid.DataSource = RipDatabaseTable.fill_ripDatabase_table(_st.RipDatabase);
        }
    }
}
