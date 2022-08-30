using System;
namespace RecipeApp.Domain.Validations.Core
{
    public class IdValidator
    {
        public bool IsValid(int? id)
        {
            if (id <= 0) throw new ArgumentException("Id cannot be set to 0.");
            else if(id == null) throw new ArgumentNullException();
            else return true;
        }
    }
}
