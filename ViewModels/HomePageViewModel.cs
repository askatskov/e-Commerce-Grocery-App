using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ViewModels
{
    public class HomePageViewModel : ObservableObject
    {
        private readonly CategoryService _categoryService;

        public HomePageViewModel(CategoryService categoryService)
        {
            _categoryService = categoryService;
            Categories = new ObservableCollection<Category>();
        }

        public ObservableCollection<Category> Categories {  get; set; }

        public async Task InitializeAsync()
        {
            foreach(var category in await _categoryService.GetMainCategoriesAsync())
            {
                Categories.Add(category);
            }
        }       
    }
}
