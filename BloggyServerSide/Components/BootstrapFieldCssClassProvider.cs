using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace BloggyServerSide.Components
{
    public class BootstrapFieldCssClassProvider : FieldCssClassProvider
    {
        public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
        {
            var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();
            var isModified = editContext.IsModified(fieldIdentifier);
            return (isModified, isValid) switch
            {
                (true, true) => "form-control modified is-valid",
                (true, false) => "form-control modified is-invalid",
                (false, true) => "form-control",
                (false, false) => "form-control"
            };
        }
    }
}
