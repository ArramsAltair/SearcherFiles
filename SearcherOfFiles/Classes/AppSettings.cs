namespace SearcherOfFiles.Classes
{
    public class AppSettings
    {
        private string _defaultPath;
        public string DefaultPath
        {
            get { return _defaultPath; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                _defaultPath = value;
            }
        }

        public string SearchPattern { get; set; }
    }
}
