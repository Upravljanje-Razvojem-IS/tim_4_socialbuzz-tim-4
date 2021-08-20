using BlockUsersService.Entities;
using BlockUsersService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Services
{
    public interface IBlockingService
    {
        List<BlockDto> GetBlocks();

        BlockDto GetBlockById(Guid ID);

        BlockDto Block_User(BlockCreationDto b);

        Block ModifyBlock(BlockModifyDto b);

        void Unblock_User(UnblockDto unblock);

    }
}
