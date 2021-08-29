using AutoMapper;
using BlockUsersService.Data.Blocking;
using BlockUsersService.Data.UserMock;
using BlockUsersService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockUsersService.ErrorHandler;
using BlockUsersService.Entities;
using BlockUsersService.Data.FollowingMock;

namespace BlockUsersService.Services
{
    public class BlockingService : IBlockingService
    {
        private readonly IBlockingRepository blockingRepository1;
        private readonly IUserMockRepository userMockRepository1;
        private readonly IMapper mapper1;

        public BlockingService(IBlockingRepository blockingRepository, IUserMockRepository userMockRepository, IMapper mapper )
        {
            this.mapper1 = mapper;
            this.userMockRepository1 = userMockRepository;
            this.blockingRepository1 = blockingRepository;
        }
        public BlockDto Block_User(BlockCreationDto b)
        {
            checkUser(b.blockerID, b.blockedID);
            if (blockingRepository1.AlreadyBlock_User(b.blockerID, b.blockedID))
                throw new BlockException($"You have allready blocked user with id = {b.blockedID}, can't blocked him again!");
            Block entity = mapper1.Map<Block>(b);
            entity.Id = Guid.NewGuid();
            entity.BlockDate = DateTime.Now;

            try
            {
                var c = blockingRepository1.Block_User(entity);
                BlockDto created = mapper1.Map<BlockDto>(c);
                return created;
            }
            catch (Exception e) 
            {
                throw new BlockException(e.Message);
            }
        }

        public BlockDto GetBlockById(Guid ID)
        {
            var b = blockingRepository1.GetBlockById(ID);

            if (b == null) throw new UserException("Block does not exist, write ID again!");

            return mapper1.Map<BlockDto>(b);
        }

        public List<BlockDto> GetBlockedList(int userID)
        {
            var user = userMockRepository1.GetUserById(userID);

            if (user == null)
                throw new UserException("There is no user with that ID, try with anotherone!");

            var blockList = blockingRepository1.GetBlockedList(userID);

            if (blockList == null || blockList.Count == 0)
                throw new UserException("User with that ID is not yet blocked by any other user, try with another user!");

            return mapper1.Map<List<BlockDto>>(blockList);
        }

        public List<BlockDto> GetBlockerList(int userID)
        {
            var user = userMockRepository1.GetUserById(userID);

            if (user == null)
                throw new UserException("There is no user with that ID, try with anotherone!");
            
            var blockList = blockingRepository1.GetBlockerList(userID);

            if(blockList == null)
                throw new UserException("User with that ID haven't blocked any other user yet, try with another!");

            return mapper1.Map<List<BlockDto>>(blockList);
        }

        public List<BlockDto> GetBlocks()
        {
            var b = blockingRepository1.GetBlocks();
            return mapper1.Map<List<BlockDto>>(b);
        }

        public Block ModifyBlock(BlockModifyDto b)
        {
            
            var map = mapper1.Map<Block>(b);
            Block old = blockingRepository1.Modify(map);
            return old;

        }

        

        public void Unblock_User(UnblockDto unblock)
        {
            checkUser(unblock.blockerID, unblock.blockedID);
            if (blockingRepository1.AlreadyUnblock_User(unblock.blockerID, unblock.blockedID))
                throw new UnblockException($"You have allready unblocked user with id = {unblock.blockedID}, can't unblocked him again!");

            try
            {
                blockingRepository1.Unblock_User(unblock.blockerID, unblock.blockedID);

            }
            catch (Exception e)
            {

                throw new UnblockException("Error with unblocking, try again!" + e.Message);
            }
        }

        private void checkUser(int blockerId, int blockedId) {

            if (userMockRepository1.GetUserById(blockerId) == null) {

                throw new UserException("User doesn't exist with that id, try again!");
            }

            if (userMockRepository1.GetUserById(blockedId) == null)
            {
                throw new UserException("User doesn't exist with that id, try again!");
            }

            if (blockedId == blockerId)
            {
                throw new UserException("You can't block yourself!");
            }
        }
    }
}
