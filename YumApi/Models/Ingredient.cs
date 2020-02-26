using System.Collections.Generic;

namespace YumApi.Models
{
    public class Ingredient
    { 
        public Ingredient()
        {
            //this.CartIngredient = new HashSet<CartIngredient>();

        }

        public int Id { get; set; }
        public string IngredientName { get; set; }
        public double Price { get; set; }

        public int? CartIngredientId { get; set; }

        public int? RecipeId { get; set; }
    }
}
