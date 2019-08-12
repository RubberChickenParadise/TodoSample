using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        /// <summary>
        /// The User ID of the todo owner
        /// </summary>
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TargetCompleteDate { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedDate { get; set; }
        public bool CompletedOnTime => Completed && CompletedDate <= TargetCompleteDate;
    }
}
