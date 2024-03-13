namespace SearcherOfFiles.Classes
{
    public class AppSettings
    {
        private string _defaultPath;

        /// <summary>
        /// Свойство директории по-умолчанию с проверкой на пустоту ввода
        /// </summary>
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

        /// <summary>
        /// Свойство шаблона поиска
        /// </summary>
        public string SearchPattern { get; set; }
    }
}
