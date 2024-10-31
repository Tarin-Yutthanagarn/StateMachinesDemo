using qf4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StateMachineExercises
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Hsm.Instance.DisplayState += new Hsm.HsmDisplayHandler(UpdateStatus);
        }

        private void UpdateStatus(object sender, HsmDisplayEventArgs h)
        {
            labelStatus.Text = h.Message;
        }

        private void buttonTransitionFromAToB_Click(object sender, EventArgs e)
        {
            QEvent qEvent = new QEvent(CustomSignals.ButtonHasBeenPressed);
            Hsm.Instance.Dispatch(qEvent);
        }
    }
}
