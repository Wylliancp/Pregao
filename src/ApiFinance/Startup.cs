using System.Linq;
using ApiFinance.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;//antigo nome era Swagger.
using ApiFinance.Interface.Service;
using ApiFinance.Services;
using ApiFinance.Interface.Business;
using ApiFinance.Business;
using ApiFinance.Interface.Repository;
using ApiFinance.Repositories;
using Microsoft.AspNetCore.Http;

namespace ApiFinance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //configuração para habilitar requisições localhost
            services.AddCors();
            //comprimir o json para enviar para a tela
            services.AddResponseCompression(options => 
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {"application/json"});
            });

            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            // services.AddTransient<IPregaoService, PregaoService>();
            services.AddTransient<IPregaoBusiness, PregaoBusiness>();
            services.AddTransient<IAtivoRepository, AtivoRepository>();
            services.AddTransient<IPregaoService, PregaoService>();
            //Adicionando a documentação para API
            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ApiFinance Api", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            

            app.UseHttpsRedirection();
            //complemento documentação
            app.UseSwagger();//formato json
            //configuração para a ferramenta visual do swagger
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","ApiFinance API V1");
            });

            app.UseRouting();

            //configuração para habilitar requisições localhost - part2
            app.UseCors(x => x.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
