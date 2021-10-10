using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task_IdeaDesignGroup.Utility
{
    public class ValidPersonalIdAttribute : ValidationAttribute
    {
        private readonly byte _idLength;
        public ValidPersonalIdAttribute(byte IdLength)
        {
            _idLength = IdLength;
        }
        public override bool IsValid(object value)
        {
            return _idLength == value.ToString().Length;
        }
    }
}
