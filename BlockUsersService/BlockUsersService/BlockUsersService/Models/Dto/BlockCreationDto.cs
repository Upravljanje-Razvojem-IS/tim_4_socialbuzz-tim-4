using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Models.Dto
{
    
    public class BlockCreationDto
    {
        [Required(ErrorMessage = "Blocker ID is required, please write it!")]
        public int blockerID { get; set; }

        [Required(ErrorMessage = "Blocked ID is required, please write it!")]
        public int blockedID { get; set; }
    }
}
