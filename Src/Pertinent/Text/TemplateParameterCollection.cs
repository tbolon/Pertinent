namespace Pertinent.Text
{
    public sealed class TemplateParameterCollection
    {
        private readonly TextTemplate template;

        public TemplateParameterCollection(TextTemplate template)
        {
            this.template = template;
        }

        public TemplateParameter this[int i]
        {
            get { return this.template.ParametersInternal[i]; }
        }

        public int Count
        {
            get { return this.template.ParametersInternal.Count; }
        }

        public void ClearValues()
        {
            foreach (var parameter in this.template.ParametersInternal)
            {
                parameter.Value = null;
            }
        }
    }
}
