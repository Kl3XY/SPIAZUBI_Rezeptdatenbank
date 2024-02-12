

namespace WEB_API
{
    [Serializable]
    public class Ingredient
    {
        public string ingredientName { get; set; }
        public string ingredientAmount { get; set; }

        public Ingredient(string nm, string am) { this.ingredientName = nm; this.ingredientAmount = am;}
    }
    [Serializable]
    public class steps
    {
        public string stepDesc { get; set; }
        public int stepNumber { get; set; }
        public steps(int nm, string des) { this.stepDesc = des; this.stepNumber = nm; }
    }
    [Serializable]
    public class Recipe
    {
        public string recipeName { get; set; }
        public string recipeDescription { get; set;}
        public List<Ingredient> ingredients = new();
        public List<steps> steps = new();


    }
}
