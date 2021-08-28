using System;

namespace ChatService.DTOs
{
    public class UserReadDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// user name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// user last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// user email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// user password
        /// </summary>
        public string Password { get; set; }
    }
}
