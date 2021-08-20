using BlockUsersService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Data.Blocking
{
    public class BlockingRepository : IBlockingRepository
    {
        private readonly BlockUserDBContext context1;
        public BlockingRepository(BlockUserDBContext context)
        {
            this.context1 = context;
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

        public Block Modify(Block block)
        {
            var exist = GetBlockById(block.Id);
             

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
