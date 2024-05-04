using System.ComponentModel.DataAnnotations;

namespace BudgetBitesAPI.Core.Models
{
    public class MealPlanModel
    {
        [Key]
        public int PersonID { get; set; }
        public string? Username { get; set; }
        public List<MealDetails> MealListDetails { get; set; } = [];
        public DateTime? WeekStartDate { get; set; }
        public DateTime? WeekEndDate { get; set; }
        public decimal TotalCost { get; set; } = 0;
    }

    public class MealDetails
    {
        [Key]
        public DateTime? PlanDate { get; set; }
        public string? MealType { get; set; }
        public List<DishDetails>? DishList { get; set; } = [];
    }
    public class DishDetails
    {
        [Key]
        public int MealPlanID { get; set; }
        public string? DishName { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public string? IsFavourite { get; set; }
        public string? ShortDiscription { get; set; }
        public string? CuisineType { get; set; }
        public int Serves { get; set; }
        public decimal Cost { get; set; }
    }

    public class MealDishModel
    {
        [Key]
        public int MealPlanID { get; set; }
        public string? Username { get; set; }
        public string? Mealtype { get; set; }
        public List<MealDishes> MealDishList { get; set; } = [];
    }

    public class MealDishes
    {
        public DateTime PlanDate { get; set; }
        public string? DishName { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public string? IsFavourite { get; set; }
        public string? ShortDiscription { get; set; }
        public string? CuisineType { get; set; }
        public int Serves { get; set; }
        public decimal Cost { get; set; }
    }

    public class DeleteDishModel
    {
        [Key]
        public int MealPlanID { get; set; }
        public string? Username { get; set; }
    }
}
