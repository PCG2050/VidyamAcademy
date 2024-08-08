using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VidyamAcademy.Handlers
{
    public class PaymnetSuccessfulMessage : ValueChangedMessage<Subject>
    {
        public PaymnetSuccessfulMessage(Subject subject) : base(subject)
        {
            
        }
    }
}
