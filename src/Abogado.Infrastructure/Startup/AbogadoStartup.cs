﻿using Abogado.Application.CasesServices.CreateCase;
using Abogado.Application.CasesServices.GetAllCasesByUser;
using Abogado.Application.CasesServices.GetByCaseId;
using Abogado.Application.CasesServices.ModifyCase;
using Abogado.Application.MeetingServices.CreateMeeting;
using Abogado.Application.MeetingServices.GetAllMeetingsByUserId;
using Abogado.Application.MeetingServices.GetAllMeetingsByUserName;
using Abogado.Application.MeetingServices.GetMeetingById;
using Abogado.Application.MeetingServices.ModifyMeeting;
using Abogado.Application.UsersServices.GetAllUsersByName;
using Abogado.Application.UsersServices.GetUserId;
using Abogado.Application.UsersServices.ModifyUser;
using Abogado.Application.UsersServices.Register;
using Abogado.Domain.Ports;
using Abogado.Infrastructure.Mappings;
using Abogado.Infrastructure.Persistences.BlobFile;
using Abogado.Infrastructure.Persistences.EF;
using Abogado.Infrastructure.Persistences.SQLServerRepository;
using Abogado.Infrastructure.Securities;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Abogado.Infrastructure.Startup
{
    public class AbogadoStartup
    {
        public static void SetUp(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureContext(services, configuration);
            ConfigureIOC(services);
            ConfigureMediador(services);
            ConfigureMapper(services);
            ConfigureAutentication(services);
        }

        private static void ConfigureContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AbogadoDbContext>(options =>
            options.UseSqlServer(
                    configuration.GetConnectionString("AbogadoConnectionString")));
        }

        private static void ConfigureIOC(IServiceCollection services)
        {
            services.AddScoped<IRepository, SQLRepository>();
            services.AddScoped<ISecurity, Security>();
            services.AddScoped<IMapObject, MapObject>();
            services.AddScoped<IRepositoryDocument, RepositoryDocumnet>();
        }

        private static void ConfigureMediador(IServiceCollection services)
        {
            services.AddMediatR(
                typeof(CreateCaseCommand).GetTypeInfo().Assembly,
                typeof(GetAllCasesByUserQuery).GetTypeInfo().Assembly,
                typeof(GetByCaseIdQuery).GetTypeInfo().Assembly,
                typeof(ModifyCaseCommand).GetTypeInfo().Assembly,
                typeof(CreateMeetingCommand).GetTypeInfo().Assembly,
                typeof(GetAllMeetingsByUserName).GetTypeInfo().Assembly,
                typeof(GetMeetingByIdQuery).GetTypeInfo().Assembly,
                typeof(ModifyMeetingCommand).GetTypeInfo().Assembly,
                typeof(GetAllUsersByNameQuery).GetTypeInfo().Assembly,
                typeof(GetUserIdQuery).GetTypeInfo().Assembly,
                typeof(ModifyUserCommand).GetTypeInfo().Assembly,
                typeof(RegisterCommand).GetTypeInfo().Assembly,
                typeof(GetCaseByUserDTO).GetTypeInfo().Assembly
                );
        }

        private static void ConfigureMapper(IServiceCollection services)
        {
            //mapeo de entidades
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        //Configura Autenticacion por cookies
        private static void ConfigureAutentication(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Users/Login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.AccessDeniedPath = "/Users/Login";
                });
        }
    }
}
