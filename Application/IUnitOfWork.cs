using Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork
    {
        IUserRepository userRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
