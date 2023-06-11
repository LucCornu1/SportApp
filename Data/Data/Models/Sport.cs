namespace SportApplication.Data.Models
{
	public class Sport : Entity
	{
        public Sport() { }

        public Sport(string sportname)
        {
            this.Name = sportname;
        }

        public string Name { get; set; } = string.Empty;
	}
}
