﻿namespace WNZDotNetTraining.RestApi.DataModels
{
    public class BlogDataModels
    {
        public int BlogId { get; set; } 
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
