namespace MindHarbor.GenClassLib.Configuration
{
    public class Property
    {
        private string name;
        private string value;
        private int editLevel = 0;
        private string optionValues;

        public Property(string name,string value)
        {
            Name = name;
            Value = value;
        }

        public Property(string name, string value, int editLevel, string optionValues) : this(name, value)
        {
            EditLevel = editLevel;
            OptionValues = optionValues;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public int EditLevel
        {
            get { return editLevel; }
            set { editLevel = value; }
        }

        public string OptionValues
        {
            get { return optionValues; }
            set { optionValues = value; }
        }
    }
}
