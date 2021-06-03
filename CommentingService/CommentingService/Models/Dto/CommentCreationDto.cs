namespace CommentingService.Models.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// DTO za dodavanje novog komentara
    /// </summary>
    public class CommentCreationDto
    {
        /// <summary>
        /// ID objave na koju se dodaje komentar
        /// </summary>
        public int PostID { get; set; }

        /// <summary>
        /// Tekst komentara
        /// </summary>
        [Required(ErrorMessage = "Comment text is required.")]
        public String CommentText { get; set; }
    }
}
