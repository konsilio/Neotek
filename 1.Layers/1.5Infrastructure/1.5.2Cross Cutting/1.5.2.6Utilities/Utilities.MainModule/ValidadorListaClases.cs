using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.DataAnnotations
{
    public class EnsureMinimumElementsAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        private readonly int _minElements;
        public EnsureMinimumElementsAttribute(int minElements)
        {
            _minElements = minElements;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count >= _minElements;
            }
            return false;
        }
    }
}
