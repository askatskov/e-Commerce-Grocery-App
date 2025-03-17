using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ViewModels
{
	public partial class HomePageViewModel : ObservableObject
	{
		private readonly CategoryService _categoryService;
		private readonly OffersService _offersService;
		public HomePageViewModel(CategoryService categoryService, OffersService offersService)
		{
			_categoryService = categoryService;
		}
		public ObservableCollection<Category> Categories { get; set; } = new();
		public ObservableCollection<Offer> Offers { get; set; } = new();

		public async Task InitializeAsync()
		{
			var offersTask = _offersService.GetActiveOffersAsync();
			foreach (var category in await _categoryService.GetCategoriesAsync())
			{
				Categories.Add(category);
			}
			foreach (var offer in await offersTask)
			{
				Offers.Add(offer);
			}
		}
	}
}