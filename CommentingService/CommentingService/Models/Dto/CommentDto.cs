namespace CommentingService.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// DTO za model komentara
    /// </summary>
    public class CommentDto
    {
        /// <summary>
        /// ID komentara
        /// </summary>
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
        /// Tekst komentara
        /// </summary>
        [Required(ErrorMessage = "Comment text is required.")]
        public String CommentText { get; set; }

        /// <summary>
        /// Datum kada je dodat komentar
        /// </summary>
        [Required(ErrorMessage = "Comment date is required.")]
        public DateTime CommentDate { get; set; }
    }
}
