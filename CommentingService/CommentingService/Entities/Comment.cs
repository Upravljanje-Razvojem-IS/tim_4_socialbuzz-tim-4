namespace CommentingService.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Model komentara na objavu
    /// </summary>
    [Table("Comments")]
    public class Comment
    {
        /// <summary>
        /// ID komentara
        /// </summary>
        [Key]
        public Guid CommentID { get; set; }

        /// <summary>
        /// ID objave na koju se dodaje komentar
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// ID korisnika koji dodaje komentar
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Text komenatara
        /// </summary>
        [Required(ErrorMessage = "Comment text is required.")]
        public String CommentText { get; set; }

        /// <summary>
        /// Datum kada je kreiran komentar
        /// </summary>
        [Required(ErrorMessage = "Comment date is required.")]
        public DateTime CommentDate { get; set; }
    }
}
