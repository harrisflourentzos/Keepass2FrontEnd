using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Keepass2.Model;
using Keepass2.Utilities;
using Newtonsoft.Json;

namespace Keepass2.Storage
{
    public class JsonRepository : IRepository
    {
        public void Save(Safe safe)
        {
            #region Safe->TempSafe
            TempSafe tempSafe = new TempSafe();

            Collection<string> tempGroups = safe.Groups;
            Dictionary<string, Collection<Credential>> tempContent = new Dictionary<string, Collection<Credential>>();
            foreach (string group in tempGroups)
            {
                tempContent.Add(group, safe[group]);
            }

            tempSafe.Name = safe.Name;
            tempSafe.Password = safe.Password.SecureStringToString();
            tempSafe.Content = tempContent;
            #endregion

            using (StreamWriter file = File.CreateText(@safe.Location + "/" + safe.Name + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, tempSafe);
            }
        }

        public Safe Load(string location)
        {
            // deserialize directly from a file
            using (StreamReader file = File.OpenText(@location))
            {
                JsonSerializer serializer = new JsonSerializer();
                TempSafe tempSafe = (TempSafe)serializer.Deserialize(file, typeof(TempSafe));

                #region TempSafe-> Safe

                Safe safe = new Safe();
                Dictionary<string, Collection<Credential>>.KeyCollection tempGroups = tempSafe.Content.Keys;

                foreach (string group in tempGroups)
                {
                    safe.AddGroup(group);
                    foreach (Credential credential in tempSafe.Content[group])
                    {
                        safe[group].Add(credential);
                    }
                }

                safe.Name = tempSafe.Name;
                safe.Password = tempSafe.Password.StringToSecureString();
                safe.Location = location.Substring(0, location.LastIndexOf("\\"));

                #endregion

                return safe;
            }


        }

        

        internal class TempSafe
        {
            public string Name { get; set; }
            public string Password { get; set; }
            public Dictionary<string, Collection<Credential>> Content { get; set; }
        }
    }
}
