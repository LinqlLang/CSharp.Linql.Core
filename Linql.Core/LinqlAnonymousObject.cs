using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linql.Core
{

    public class LinqlAnonymousProperty: LinqlProperty
    {
        public LinqlExpression Value { get; set; }

        public LinqlAnonymousProperty() { }

        public LinqlAnonymousProperty(string PropertyName, LinqlExpression Value): base(PropertyName)
        {
            this.Value = Value;
        }

        public override string ToString()
        {
            return $"LinqlAnonymousProperty {this.PropertyName}";
        }

        public override bool Equals(object obj)
        {
            if (obj is LinqlAnonymousProperty casted)
            {
                return
                    this.PropertyName.Equals(casted.PropertyName)
                    && this.Value.Equals(casted.Value)
                    && base.Equals(obj);
            }
            return false;
        }

        public string GetHashKey(Type PropType)
        {
            return $"{this.PropertyName}:{PropType.Name}";
        }
    }


    /// <summary>
    /// Represents a LinqlAnonymousObject.  LinqlAnonymousObjects are serialized as is, and do not resolve internally.  
    /// For instance, given the expression SomeObject.SomeProperty, if SomeObject is defined as a LinqlAnonymousObject, SomeObject would be serialized in whole.  The property traversal will then be resolved on the server.  
    /// This is distinctly different if it were defined as a constant, which will resolve on the client first.
    /// </summary>
    public class LinqlAnonymousObject : LinqlExpression
    {
        public List<LinqlAnonymousProperty> Properties { get; set; }

        /// <summary>
        /// This constructor is required for Json serialization/deserialization.  Should probably not use this.
        /// </summary>
        public LinqlAnonymousObject() { }

        public void AddProperty(string PropertyName, LinqlExpression Value)
        {
            if (this.Properties == null)
            {
                this.Properties = new List<LinqlAnonymousProperty>();
            }
            LinqlAnonymousProperty prop = new LinqlAnonymousProperty(PropertyName, Value);
            this.Properties.Add(prop);
        }

        public override bool Equals(object obj)
        {
            if (obj is LinqlAnonymousObject casted)
            {
                if(casted.Properties?.Count() != this.Properties?.Count())
                {
                    return false;
                }

                bool propsMatch = true;
                int index = 0;
                foreach(LinqlAnonymousProperty prop in this.Properties)
                {
                    propsMatch = casted.Properties[0].Equals(prop);

                    if(!propsMatch)
                    {
                        break;
                    }
                }
                return
                    propsMatch
                    && base.Equals(obj);
            }
            return false;
        }

        public string GetHashKey(IEnumerable<Type> PropTypes)
        {
            var zip = this.Properties.Zip(PropTypes, (left, right) => new { LinqlProp = left, PropType = right });
            IEnumerable<string> hashKeys = zip.Select(r => r.LinqlProp.GetHashKey(r.PropType));
            return String.Join(";", hashKeys);
        }

    }

}
