using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FruitWebApp.Models;
public class MockFruitModel 
{

      [Key]
      public int id { get; set; }

      [Display(Name = "Fruit Name")]
      public string? name { get; set; }

      [Display(Name="Is Available?")]
      public bool instock { get; set; }

}