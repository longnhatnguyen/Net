using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CyberBook_API.Dal.Repositories
{
    public class AdminsRepository : GenericRepository<Models.User>, IAdminsRepository
    {
        //public Task<Models.User> Create(Models.User entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> Delete(Models.User entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Models.User>> FindBy(Expression<Func<Models.User, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> Update(Models.User entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> Update(Models.User entity, int key)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<Models.User>> IGenericRepository<Models.User>.GetAll()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
