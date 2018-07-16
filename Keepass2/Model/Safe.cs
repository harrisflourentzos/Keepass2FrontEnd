using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Keepass2.Model
{
    public class Safe
    {
        private readonly Dictionary<string, ObservableCollection<Credential>> _contents = new Dictionary<string, ObservableCollection<Credential>>();

        public string Name { get; set; }
        public string Password { get; set; }
        public ObservableCollection<string> Groups { get; } = new ObservableCollection<string>();

        public void AddGroup(string name)
        {
            if (!Groups.Contains(name)) Groups.Add(name);
            _contents[name] = new ObservableCollection<Credential>();
        }

        public void RemoveGroup(string name)
        {
            Groups.Remove(name);
            _contents.Remove(name);
        }

        public ObservableCollection<Credential> this[string group] => _contents[group];
    }
}
