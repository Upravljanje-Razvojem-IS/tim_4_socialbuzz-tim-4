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
    }
}
