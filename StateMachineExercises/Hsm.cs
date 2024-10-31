using qf4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineExercises
{
    class Hsm : QHsm
    {
        private QState superState1;
        private QState stateA;
        private QState stateB;

        public delegate void HsmDisplayHandler(object sender, HsmDisplayEventArgs e);
        public event HsmDisplayHandler DisplayState;

        public Hsm()
        {
            superState1 = new QState(this.SuperState1);
            stateA = new QState(this.A);
            stateB = new QState(this.B);
        }

        private static volatile Hsm singleton = null;
        private static object sync = new object();//for static lock

        public static Hsm Instance
        {
            get
            {
                if (singleton == null)
                {
                    lock (sync)
                    {
                        if (singleton == null)
                        {
                            singleton = new Hsm();
                            singleton.Init();
                        }
                    }
                }

                return singleton;
            }
        }//Instance

        protected override void InitializeStateMachine()
        {
            InitializeState(superState1);
        }

        private QState SuperState1(IQEvent qEvent)
        {
            if (qEvent.IsSignal(QSignals.Entry))
            {
                OnDisplayState("Enter superState1");
                return null;
            }

            if (qEvent.IsSignal(QSignals.Init))
            {
                InitializeState(stateA);
                return null;
            }

            if (qEvent.IsSignal(QSignals.Exit))
            {
                OnDisplayState("Exit superState1");
                return null;
            }

            return TopState;
        }

        private QState A(IQEvent qEvent)
        {
            if (qEvent.IsSignal(QSignals.Entry))
            {
                OnDisplayState("Enter A");
                return null;
            }

            if (qEvent.IsSignal(CustomSignals.ButtonHasBeenPressed))
            {
                TransitionTo(stateB);
                return null;
            }

            if (qEvent.IsSignal(QSignals.Entry))
            {
                OnDisplayState("Enter B");
                return null;
            }

            return superState1;
        }

        private QState B(IQEvent qEvent)
        {
            if (qEvent.IsSignal(QSignals.Entry))
            {
                OnDisplayState("Enter B");
                return null;
            }

            if (qEvent.IsSignal(QSignals.Entry))
            {
                OnDisplayState("Exit B");
                return null;
            }

            return superState1;
        }

        private void OnDisplayState(string stateInfo)
        {
            if (DisplayState != null)
            {
                DisplayState(this, new HsmDisplayEventArgs(stateInfo));
            }
        }
    }

    public class HsmDisplayEventArgs : EventArgs
    {
        private string s;
        public string Message { get { return s; } }

        public HsmDisplayEventArgs(string message) { s = message; }
    }
}
