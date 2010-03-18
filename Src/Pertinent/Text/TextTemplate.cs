namespace Pertinent.Text
{
    using System.Collections.Generic;
    using System.Text;

    public class TextTemplate
    {
        private readonly string source;

        private readonly List<TemplateParameter> parameters = new List<TemplateParameter>();

        private TemplateParameterCollection parameterCollection;

        public TextTemplate(string source)
        {
            this.source = source;

            this.parameterCollection = new TemplateParameterCollection(this);

            this.Parse();
        }

        public TemplateParameterCollection Parameters
        {
            get { return this.parameterCollection; }
        }

        internal IList<TemplateParameter> ParametersInternal
        {
            get { return this.parameters; }
        }

        public string Execute()
        {
            var result = new StringBuilder(this.source.Length);
            var count = this.parameters.Count;
            var previousIndex = 0;

            for (int i = 0; i < count; i++)
            {
                var parameter = this.parameters[i];

                if (parameter.Index > previousIndex)
                {
                    result.Append(this.source.Substring(previousIndex, parameter.Index - previousIndex));
                }

                result.Append(parameter.FormatValue());

                previousIndex = parameter.Index + parameter.Length;
            }

            if (previousIndex < this.source.Length)
            {
                result.Append(this.source.Substring(previousIndex));
            }

            return result.ToString();
        }

        private void Parse()
        {
            int length = this.source.Length;
            bool escaped = false;

            int parameterStart = -1;
            int parameterIndex = 0;

            string parameterName = null;
            string parameterDefaultValue = null;
            string parameterFormat = null;

            List<char> chars = new List<char>();

            for (int i = 0; i < length; i++)
            {
                var c = this.source[i];

                if (escaped)
                {
                    escaped = false;
                    if (parameterStart != -1)
                    {
                        chars.Add(c);
                    }
                    continue;
                }

                if (c == '\\')
                {
                    escaped = true;
                    continue;
                }

                if (parameterStart == -1)
                {
                    // Outside parameter
                    if (c == '[')
                    {
                        // New parameter start
                        parameterStart = i;
                        parameterName = null;
                        parameterDefaultValue = null;
                        parameterFormat = null;
                        chars.Clear();
                    }
                }
                else
                {
                    // Inside parameter
                    if (c == ':' && i > parameterStart + 1)
                    {
                        if (parameterIndex == 0)
                        {
                            parameterName = new string(chars.ToArray());
                        }
                        else if (parameterIndex == 1)
                        {
                            parameterDefaultValue = new string(chars.ToArray());
                        }
                        else if (parameterIndex == 2)
                        {
                            parameterFormat = new string(chars.ToArray());
                        }

                        parameterIndex++;
                        chars.Clear();
                    }
                    else if (c == ']' && parameterStart != -1)
                    {
                        if (i > parameterStart + 1)
                        {
                            if (parameterIndex == 0)
                            {
                                parameterName = new string(chars.ToArray());
                            }
                            else if (parameterIndex == 1)
                            {
                                parameterDefaultValue = new string(chars.ToArray());
                            }
                            else if (parameterIndex == 2)
                            {
                                parameterFormat = new string(chars.ToArray());
                            }

                            this.parameters.Add(new TemplateParameter(parameterStart, i - parameterStart + 1, parameterName, parameterDefaultValue, parameterFormat));

                            parameterStart = -1;
                        }
                    }
                    else
                    {
                        if (parameterStart != -1)
                        {
                            chars.Add(c);
                        }
                    }
                }
            }
        }
    }
}
