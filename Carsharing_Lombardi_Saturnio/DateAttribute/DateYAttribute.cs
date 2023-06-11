using System.ComponentModel.DataAnnotations;

namespace Carsharing_Lombardi_Saturnio.DateAttribute
{
    public class DateYAttribute : ValidationAttribute
    {
        public DateYAttribute() { }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if (dt >= DateTime.Today)
            {
                return true;
            }
            return false;
        }
    }
}
