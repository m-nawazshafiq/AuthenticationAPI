using AppDataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Func<T, bool> filter);
    }
}
