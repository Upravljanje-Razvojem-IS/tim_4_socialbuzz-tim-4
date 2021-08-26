using BlockUsersService.Data.FollowingMock;
using BlockUsersService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockUsersService.ErrorHandler;

namespace BlockUsersService.Data.Blocking
{
    public class BlockingRepository : IBlockingRepository
    {
        private readonly BlockUserDBContext context1;

        public BlockingRepository(BlockUserDBContext context)
        {
            this.context1 = context;
        }

        public bool AlreadyBlock_User(int userID, int blockedID)
        {
            var blocks = GetBlocks();
            foreach (var item in blocks)
            {
                if(item.BlockedId == userID && item.BlockerId == blockedID) // User blocked me
                {
                    return true;
                }
                else if(item.BlockerId == userID && item.BlockedId == blockedID) // I blocked user
                {
                    return true;
                }

                
            }
            return false;
        }

        public bool AlreadyUnblock_User(int userID, int blockedID)
        {
            var query = from b in context1.Blocks
                        select b;

            foreach (var item in query)
            {
                if ( item.BlockerId == userID && item.BlockedId == blockedID) 
                {
                    return false;
                }
                else if (item.BlockedId == userID && item.BlockerId == blockedID) 
                {
                    return false;
                }


            }
            return true;
        }

        public Block Block_User(Block blok)
        {
            context1.Add(blok);
            context1.SaveChanges();
            return blok;
        }

        
        public Block GetBlockById(Guid ID)
        {
            return context1.Blocks.Find(ID);
        }

        public List<Block> GetBlocks()
        {
            return context1.Blocks.ToList();
        }

        public List<Block> GetBlockerList(int userID)
        {
            var q = from block in context1.Blocks
                    where block.BlockerId == userID
                    select block;
            return q.ToList();
        }

        public List<Block> GetBlockedList(int userID)
        {
            var q = from block in context1.Blocks
                    where block.BlockedId == userID
                    select block;
            return q.ToList();
        }

        public Block Modify(Block block)
        {
            var exist = GetBlockById(block.Id);

            if (block.BlockerId == block.BlockedId)
                throw new BlockException("You can't block youreself!");

            if(AlreadyBlock_User(block.BlockerId, block.BlockedId))
                throw new BlockException("You allready block this user, you can't block him again!");


            try
            {
                exist.BlockerId = block.BlockerId;
                exist.BlockedId = block.BlockedId;
                exist.BlockDate = DateTime.Now;
                context1.Blocks.Update(exist);
                context1.SaveChanges();
                return exist;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public void Unblock_User(int blockerID, int blockedID)
        {
            Block b = context1.Blocks.FirstOrDefault(b => b.BlockerId == blockerID && b.BlockedId == blockedID);
            context1.Remove(b);
            context1.SaveChanges();
        }

        
    }
}
