namespace Lapis.API.Models
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string GetConnectionString();
    }

    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Return connection string replaced data
        /// </summary>
        /// <returns>connection string</returns>
        public string GetConnectionString()
        {
            ConnectionString = ConnectionString.Replace("<username>", Username);
            ConnectionString = ConnectionString.Replace("<password>", Password);
            ConnectionString = ConnectionString.Replace("<database>", DatabaseName);
            return ConnectionString;
        }
    }
}