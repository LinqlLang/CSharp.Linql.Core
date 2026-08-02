using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linql.Core
{
    /// <summary>
    /// Represents a Condition expression (ternary) (Binary ? ifTrue : ifFalse ).  
    /// </summary>
    public class LinqlCondition : LinqlExpression
    {
        /// <summary>
        /// This constructor is required for Json serialization/deserialization.  Should probably not use this.
        /// </summary>
        public LinqlCondition() { }


        public LinqlBinary Binary { get; set; }

        public LinqlExpression ifTrue { get; set; }

        public LinqlExpression ifFalse { get; set; }

     
        public LinqlCondition(LinqlBinary Binary, LinqlExpression ifTrue, LinqlExpression ifFalse) 
        {
            this.Binary = Binary;
            this.ifTrue = ifTrue;
            this.ifFalse = ifFalse;
        }

        public override string ToString()
        {
            return $"LinqlCondition";
        }

        public override bool Equals(object obj)
        {
            if(obj is LinqlCondition compare)
            {

                return this.Binary.Equals(compare.Binary)
                    && this.ifTrue.Equals(compare.ifTrue)
                    && this.ifFalse.Equals(compare.ifFalse);
                
            }
            return false;
        }

        public override bool IsMatch(LinqlExpression ExprssionToCompare, LinqlFindOption FindOption = LinqlFindOption.Exact)
        {
            if (ExprssionToCompare is LinqlCondition compare)
            {
                return this.Binary.Equals(compare.Binary)
                    && this.ifTrue.Equals(compare.ifTrue)
                    && this.ifFalse.Equals(compare.ifFalse);
            }

            return false;
        }
    }
}
