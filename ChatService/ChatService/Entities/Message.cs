using System;
using System.ComponentModel.DataAnnotations;

namespace ChatService.Entities
{
    public class Message
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Created
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Senderid
        /// </summary>
        public Guid SenderId { get; set; }
        /// <summary>
        /// Sender
        /// </summary>
        public Guid ReciverId { get; set; }
    }
}
