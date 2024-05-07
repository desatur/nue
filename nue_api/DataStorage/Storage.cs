namespace nue.DataStorage
{
    internal class Storage
    {
        public Storage()
        {
            string dataDirectory = "data";
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
        }
    }
}
