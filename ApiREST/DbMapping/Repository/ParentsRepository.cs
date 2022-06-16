﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public class ParentsRepository : IParentsRepository<Parents>
    {
        readonly MyContext _myContext;

        public ParentsRepository(MyContext context)
        {
            _myContext = context;
        }
        public ICollection<Parents> GetAllParents()
        {
            return _myContext.Rodzice.ToList();
        }
        public Parents GetParent(string email, string password)
        {
            return _myContext.Rodzice.Include(p => p.Parents_students).FirstOrDefault(s => (s.email == email && s.passwd == password));
        }
    }
}