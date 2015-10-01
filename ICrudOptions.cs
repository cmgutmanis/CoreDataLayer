﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreData
{
    public interface ICrudOptions<TItem>
    {
        TItem Get(int id);

        IEnumerable<TItem> GetAll();

        int Add(TItem item);

        int Add(TItem item, string sprocCommand);

        void Update(TItem item);

        void Delete(int id);
    }
}
