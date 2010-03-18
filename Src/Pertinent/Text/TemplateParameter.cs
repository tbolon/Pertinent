namespace Pertinent.Text
{
    public class TemplateParameter
    {
        private readonly int index;

        private readonly int length;

        private readonly string name;

        private readonly string defaultValue;

        private readonly string format;

        public TemplateParameter(int index, int length, string name, string defaultValue, string format)
        {
            this.index = index;
            this.length = length;
            this.name = name;
            this.defaultValue = defaultValue;
            this.format = format;
        }

        public int Index
        {
            get { return this.index; }
        }

        public int Length
        {
            get { return this.length; }
        }

        public string Value { get; set; }

        public string Name
        {
            get { return this.name; }
        }

        public string DefaultValue
        {
            get { return this.defaultValue; }
        }

        public string Format
        {
            get { return this.format; }
        }

        public string FormatValue()
        {
            var value = this.Value;

            if (value == null)
            {
                value = this.defaultValue;
            }

            return this.FormatValue(this.Value ?? this.defaultValue);
        }

        private string FormatValue(string value)
        {

            if (string.IsNullOrEmpty(this.format))
            {
                return value;
            }

            return string.Format(this.format, value);
        }
    }
}