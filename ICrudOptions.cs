using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreData
{
    public interface ICrudOptions<TItem>
    {
        TItem Get(int id);

        IEnumerable<TItem> GetAll();

        void Add();

        void Add(string sprocCommand);

        void Update(string sprocCommand);

        void Update();

        void Delete(int id);

        ICrudOptions<TItem> MapAllExcept(List<string> exclusions);
    }
}
