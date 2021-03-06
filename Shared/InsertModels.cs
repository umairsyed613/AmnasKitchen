﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApnaBawarchiKhana.Shared
{
    public class CreateCategoryFormData
    {
        public Category Category { get; set; }

        public string IconPath { get; set; }
    }
    
    public class CreateRecipeFormData
    {
        public RecipeForm Recipe { get; set; }
        
        public List<string> Images { get; set; }
    }

    public class RecipeForm
    {
        [MinLength(1, ErrorMessage = "Please choose category")]
        public List<int> SelectedCategoriesIds { get; set; } //= new List<int>();

        [Range(1, 60, ErrorMessage = "Please enter time to prepare the recipe")]
        public int Time { get; set; }

        [Required(ErrorMessage = "Enter a TimeUnit")]
        public string TimeUnit { get; set; }

        [Range(1, 5, ErrorMessage = "Choose difficulty level")]
        public int Difficulty { get; set; }

        [Required(ErrorMessage = "Enter servings")]
        public int Serving { get; set; }

        [Required(ErrorMessage = "Enter a Recipe Title")]
        public string Title { get; set; }

        public string Description { get; set; }

        [MinLength(1, ErrorMessage = "Please Add Ingredients")]
        public List<Ingredient> Ingredients { get; set; }

        [MinLength(1, ErrorMessage = "Please Add Directions")]
        public List<Direction> Directions { get; set; }
    }

    public class RecipeRatingForm
    {
        [Required(ErrorMessage = "Provide Recipe Id")]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Provide Rating")]
        public int RatingValue { get; set; }
    }
}
