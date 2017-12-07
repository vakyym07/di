using System;

namespace TagsCloudVisualization
{
    public class TagValidatorException : Exception
    {
        public TagValidatorException(string message)
            : base(message)
        {
        }
    }
}