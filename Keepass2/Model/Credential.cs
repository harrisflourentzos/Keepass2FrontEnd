namespace Keepass2.Model
{
    public class Credential
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Notes { get; set; }

        public Credential(string Title, string UserName, string Password, string Url, string Notes)
        {
            this.Title = Title;
            this.UserName = UserName;
            this.Password = Password;
            this.Url = Url;
            this.Notes = Notes;

        }
    }
}
