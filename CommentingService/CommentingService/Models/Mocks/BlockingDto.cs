namespace CommentingService.Models
{
    using System;

    /// <summary>
    /// DTO za model blokiranja korisnika
    /// </summary>
    public class BlockingDto
    {
        /// <summary>
        /// ID blocka
        /// </summary>
        private Guid blockingID;

        public Guid BlockingID
        {
            get { return blockingID; }
            set { blockingID = value; }
        }

        /// <summary>
        /// ID korisnika koji blokira
        /// </summary>
        private int blockerID;

        public int BlockerID
        {
            get { return blockerID; }
            set { blockerID = value; }
        }

        /// <summary>
        /// ID korisnika koji se blokira
        /// </summary>
        private int blockedID;

        public int BlockedID
        {
            get { return blockedID; }
            set { blockedID = value; }
        }
    }
}
