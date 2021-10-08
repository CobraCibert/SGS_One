using Sistemas.Core.Entities.Models;
using Sistemas.Core.Interfaces;
using Sistemas.Core.Services.Interfaces;
using System.Collections.Generic;

namespace Sistemas.Core.Services.Repositories
{
    public class BancoService : IBancoService
    {
        private IUnitOfWork _unitOfWork;
        public BancoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Banco Get(int id)
        {
            using (var db = _unitOfWork.Create())
            {
                var result = db.Repositories.BancoRepository.Get(id);
                return result;
            }
        }

        public IEnumerable<Banco> GetAll()
        {
            using var db = _unitOfWork.Create();
            var records = db.Repositories.BancoRepository.GetAll();
            return records;
        }

        public void Create(Banco model)
        {
            using (var db = _unitOfWork.Create())
            {
                db.Repositories.BancoRepository.Create(model);
                db.SaveChanges();
            }
        }

        public void Update(Banco model)
        {
            using (var db= _unitOfWork.Create())
            {
                db.Repositories.BancoRepository.Update(model);
                db.SaveChanges();
            }
        }
    }
}
