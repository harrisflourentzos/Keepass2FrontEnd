using System.Collections.Generic;
using System.Linq;

namespace Keepass2.Model
{
    public class Safe
    {
        private Dictionary<string, List<Credential>> contents = new Dictionary<string, List<Credential>>();

        public string Name { get; set; }
        public string Password { get; set; }
        public List<string> Groups => contents.Keys.ToList();

        public void AddGroup(string name) => contents[name] = new List<Credential>();
        public void RemoveGroup(string name) => contents.Remove(name);

        public List<Credential> this[string group] => contents[group];
    }
}
