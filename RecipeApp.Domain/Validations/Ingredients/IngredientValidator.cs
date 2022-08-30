using System;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Domain.Validations.Ingredients
{
    public class IngredientValidator
    {
        
        public bool NoNulls(Ingredient ingredient)
        {
            if (ingredient.Name == null
                || ingredient.Name == ""
                || ingredient.Id == 0) return false;
            else return true;
        }

        public bool IngCompoNoNulls(IngredientComposition ingCompo)
        {
            if (ingCompo?.IngredientId <= 0
                || ingCompo?.Id <= 0
                || ingCompo.Name == null
                || ingCompo.Quantity <= 0 //NULLABLE PROPERTY in IngCompo resolves the 0 assignment to null.
) throw new ArgumentNullException("Required fields!");
            return true;
        }

        public bool IngCompoNameIsValid(string name)
        {
            if (name == "") throw new ArgumentOutOfRangeException("name", "Invalid name value!");
            return true;
        }

        public bool IngCompoQuantityIsValid(double? q)
        {
            if (q <= 0) throw new ArgumentOutOfRangeException("quantity", "Invalid quantity value.");
            return true;
        }

        public bool UnitIsValid(int unit)
        {
            int[] units = new int[] {
                (int)MesurementUnits.cL,
                (int)MesurementUnits.dL,
                (int)MesurementUnits.g,
                (int)MesurementUnits.kg,
                (int)MesurementUnits.L,
                (int)MesurementUnits.mg,
                (int)MesurementUnits.mL,
                (int)MesurementUnits.pinch,
                (int)MesurementUnits.qb,
                (int)MesurementUnits.unit };

            return Array.Exists(units, u => u.Equals(unit));
        }



    }
}
