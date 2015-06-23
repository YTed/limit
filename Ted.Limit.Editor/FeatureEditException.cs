using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Editor
{
    class FeatureEditException : Exception
    {
        public FeatureEditException()
            : base()
        {

        }

        public FeatureEditException(string msg)
            : base(msg)
        {

        }

        public FeatureEditException(string msg, Exception cause)
            : base(msg, cause)
        {

        }

        public FeatureEditException(
            System.Runtime.Serialization.SerializationInfo info, 
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
