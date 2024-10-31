using qf4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineExercises
{
	public static class CustomSignals
    {
		public static readonly Signal ButtonHasBeenPressed = new Signal("ButtonHasBeenPressed");
	}
}
