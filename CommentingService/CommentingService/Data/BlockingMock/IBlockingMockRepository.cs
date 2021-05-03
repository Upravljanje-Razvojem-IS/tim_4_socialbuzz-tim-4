namespace CommentingService.Data.BlockingMock
{
    using System.Collections.Generic;

    public interface IBlockingMockRepository
    {
        List<int> GetBlockedUsers(int userId);
       
        /// <summary>
        /// Provera da li samm blokirala korisnika
        /// </summary>
        /// <param name="userId">Moj Id</param>
        /// <param name="blockedId">Id korisnika za kog proveravam da li mi je blokiran</param>
        /// <returns></returns>
        public bool CheckDidIBlockUser(int userId, int blockedId);
    }
}
