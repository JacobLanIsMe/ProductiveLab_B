using Microsoft.EntityFrameworkCore;
using ReproductiveLab_Common.Dtos.ForTransferIn;
using ReproductiveLab_Repository.Interfaces;
using ReproductiveLabDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Repository.Repositories
{
    public class TransferInRepository : ITransferInRepository
    {
        private readonly ReproductiveLabContext _db;
        public TransferInRepository(ReproductiveLabContext db)
        {
            _db = db;
        }
        public void AddTransferIn(AddTransferInDto input)
        {
            TransferIn transferIn = new TransferIn
            {
                TransferInTime = input.transferInTime
            };
            _db.TransferIns.Add(transferIn);
            _db.SaveChanges();
        }
    }
}
