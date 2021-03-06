﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security;
using Keepass2.Wizards.NewSafe;

namespace Keepass2.Model
{
    public class Safe
    {
        private readonly Dictionary<string, ObservableCollection<Credential>> _contents = new Dictionary<string, ObservableCollection<Credential>>();

        public string Name { get; set; }
        public SecureString Password { get; set; }
        public string Location { get; set; }
        public ObservableCollection<string> Groups { get; } = new ObservableCollection<string>();
        public bool PendingChanges { get; set; }

        public Safe()
        {
        }

        public Safe(NewSafeState newSafeState)
        {
            Name = newSafeState.Name;
            Password = newSafeState.MasterPassword;
            Location = newSafeState.Location;

            AddGroup("General");
            AddGroup("Internet");
            AddGroup("Email");
            AddGroup("Banking");
            AddGroup("Windows");

            //new List<Credential>
            //{
            //    new Credential("Evernote", "keepass@gmail.com", "password1234", "http://evernote.com", "notes"),
            //    new Credential("Facebook", "keepass@gmail.com", "password1234", "http://facebook.com", "notes"),
            //    new Credential("Facebook", "keepass@gmail.com", "password1234", "http://facebook.com", "notes"),
            //    new Credential("Facebook", "keepass@gmail.com", "password1234", "http://facebook.com", "notes"),
            //    new Credential("Facebook", "keepass@gmail.com", "password1234", "http://facebook.com", "notes"),
            //    new Credential("Facebook", "keepass@gmail.com", "password1234", "http://facebook.com", "notes"),
            //    new Credential("Facebook", "keepass@gmail.com", "password1234", "http://facebook.com", "notes"),
            //    new Credential("Facebook", "keepass@gmail.com", "password1234", "http://facebook.com", "notes")

            //}.ForEach(this._contents["Internet"].Add);

            //new List<Credential>
            //{
            //    new Credential("Royal Bank of Canada", "keepass@gmail.com", "password1234", "http://evernote.com", "notes"),
            //    new Credential("BankofScottland", "keepass@gmail.com", "password1234", "http://evernote.com", "notes"),
            //    new Credential("BankofCyprus", "keepass@gmail.com", "password1234", "http://facebook.com", "notes")

            //}.ForEach(this._contents["Bank"].Add);

        }

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

        public void RenameGroup(string oldName, string newName)
        {
            var index = Groups.IndexOf(oldName);

            Groups.RemoveAt(index);
            Groups.Insert(index, newName);

            var contents = _contents[oldName];

            _contents.Remove(oldName);
            _contents[newName] = contents;
        }

        public ObservableCollection<Credential> this[string group] => _contents[group];

    }
}
