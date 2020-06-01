using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Reciply.API.Models;

namespace Reciply.API.Data
{
    public class Seed
    {
        public static void SeedUsers(AppDbContext context)
        {
            if (!context.Users.Any()) // check if the db is empty
            {
                var usersFromFile = System.IO.File.ReadAllText("Data/SeedData.json");
                var users = JsonSerializer.Deserialize<List<User>>(usersFromFile);

                var ingredientNames = new List<IngredientName>
                {
                    new IngredientName{Name = "Potatoes"},
                    new IngredientName{Name = "Water"},
                    new IngredientName{Name = "Wine"},
                    new IngredientName{Name = "Orange"},
                };

                foreach (var ingredient in ingredientNames)
                {
                    context.IngredientNames.Add(ingredient);
                }

                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    GeneratePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();

                    context.Users.Add(user);
                }                
                
                foreach (var user in users)
                {
                    var ingredients = new List<Ingredient>
                    {
                        new Ingredient{ IngredientNameId = 1, Quantity=12, Unit="g"},
                        new Ingredient{ IngredientNameId = 2, Quantity=1, Unit="ml"}
                        
                        //new Ingredient{ IngredientNameId = 1, Quantity=12, Unit="g", RecipeId=user.Recipes.FirstOrDefault().RecipeId, Recipe=user.Recipes.FirstOrDefault()},
                        //new Ingredient{ IngredientNameId = 2, Quantity=1, Unit="ml", RecipeId=user.Recipes.FirstOrDefault().RecipeId, Recipe=user.Recipes.FirstOrDefault()},
                    };

                    user.Recipes.FirstOrDefault().Ingredients = ingredients;
                }

                
                context.SaveChanges();
            }
        }

        private static void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}