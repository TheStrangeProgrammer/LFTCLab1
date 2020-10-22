using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFTCLab3
{
    class ProgramInternalForm
    {
        private List<KeyValuePair<string, KeyValuePair<int, int>>> list = new List<KeyValuePair<string, KeyValuePair<int, int>>>();
        public ProgramInternalForm()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, KeyValuePair<int, int> value) {

            list.Add(new KeyValuePair<string, KeyValuePair<int, int>>(key, value));
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
