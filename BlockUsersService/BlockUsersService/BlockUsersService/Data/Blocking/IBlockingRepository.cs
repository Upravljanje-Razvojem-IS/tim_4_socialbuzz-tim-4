using BlockUsersService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Data.Blocking
{
    public interface IBlockingRepository
    {
        List<Block> GetBlocks();

        Block GetBlockById(Guid ID);

        Block Block_User(Block blok);

        Block Modify(Block block);

        void Unblock_User(int blockerID, int blockedID);

        bool AlreadyBlock_User(int userID, int blockedID);

        bool AlreadyUnblock_User(int userID, int blockedID);

        List<Block> GetBlockerList(int userID);
        List<Block> GetBlockedList(int userID);


    }
}
