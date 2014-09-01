using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace VarcalSys_System.Util.Interfaces
{
    public interface IRepository<T> where T: class
    {
        string Save(T entity);
        string Delete(T entity);
        T Find(int id);
        IEnumerable<T> FindAll();
    }
}
