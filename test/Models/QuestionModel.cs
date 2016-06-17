using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class QuestionModel
    {
        public QuestionModel() {  }
        public int Id { get; set; }
        public string Description { get; set; }
        public string RightAnswer { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }

        public string UserName { get; set; }
        public int CategoryId { get; set; }

        public virtual CategoryModels Category { get; set; }

        public virtual List<TestModels> Tests { get; set; }
    }
}