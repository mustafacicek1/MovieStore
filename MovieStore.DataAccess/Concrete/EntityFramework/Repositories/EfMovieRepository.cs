﻿using Microsoft.EntityFrameworkCore;
using MovieStore.Core.DataAccess.EntityFramework;
using MovieStore.DataAccess.Abstract;
using MovieStore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfMovieRepository:EfEntityRepositoryBase<Movie>,IMovieRepository
    {
        public EfMovieRepository(DbContext context):base(context)
        {

        }
    }
}
