namespace SportsStore.WebUI.ViewModels
{
    using System.Collections.Generic;

    public class CategoriesListViewModel
    {
        public IEnumerable<string> Categories { get; set; }

        public string CurrentCategory { get; set; }
    }
}