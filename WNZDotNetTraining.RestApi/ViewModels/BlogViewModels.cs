﻿namespace WNZDotNetTraining.RestApi.ViewModels
{
    public class BlogViewModels
    {
        public int Id { get; set; }

        public string ? Title { get; set; }

        public string  ? Author { get; set; }

        public string  ? Content { get; set; }

        public bool DeleteFlag { get; set; }
    }
}

