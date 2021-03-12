using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diary_Sample.Models
{
    public class UserAdminAccountViewModel
    {
        public string UserId { get; set; } = string.Empty;

        public string Notification { get; set; } = string.Empty;

        public bool UpdateResult { get; set; } = true;

    }

}
