namespace CommentingService.Models.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// DTO za modifikaciju komentara
    /// </summary>
    public class CommentModifyingDto
    {
        /// <summary>
        /// ID komentara koji se modifikuje
        /// </summary>
        public Guid CommentID { get; set; }

        /// <summary>
        /// ID objave na koju je dodat komentar koji se modifikuje
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// Tekst komentara
        /// </summary>
        [Required(ErrorMessage = "Comment text is required.")]
        public String CommentText { get; set; }
    }
}
