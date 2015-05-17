using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolatileTweetLibrary
{
	public class VolatileTask
	{
		public enum TaskType { NONE, PREPARE, LEARN_WORD, ESTIMATE, LEARN_VOLATILE, TWEET }
		public TaskType Task;
		public string ScreenName;
		public string InputDir;
		public string NetworkDir;
		public string Input;
	}
}
