using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class CategoryModels
    {
        public CategoryModels() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string UserName { get; set; }

        public virtual List<QuestionModel> Questions { get; set; }
    }
}