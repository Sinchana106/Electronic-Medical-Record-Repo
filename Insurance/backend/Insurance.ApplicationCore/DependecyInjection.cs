﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.ApplicationCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationCoreDI(this IServiceCollection services)
        {
            return services;
        }

    }
}
