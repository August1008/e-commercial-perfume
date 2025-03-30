using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.BaseModels;

namespace Core.Models.RequestModels
{
    public class UserCreationModel : UserBaseModel
    {
        public new required string Email { get; set; }
        public required string Password { get; set; }
    }
}
