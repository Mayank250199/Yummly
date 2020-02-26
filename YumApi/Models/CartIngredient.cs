using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YumApi.Models
{
    public class CartIngredient
    {
        public CartIngredient()
        {
            this.Ingredient = new HashSet<Ingredient>();
        }
        public int Id { get; set; }

        public ICollection<Ingredient> Ingredient { get; set; }

        public float Quantity { get; set; }

        public Unit Unit { get; set; }

        public int? UserProfileId { get; set; }
       
    }
}
