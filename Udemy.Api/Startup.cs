using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Udemy.Api.Data;

namespace udemy {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddMvc().AddJsonOptions(opcoes => // O método AddJsonOptions permite a customização das configurações de serialização
            {
                opcoes.SerializerSettings.NullValueHandling =
                Newtonsoft.Json.NullValueHandling.Ignore; // Consulta com esse comando vai ignorar valores nulos 
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
         
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options => //Gzip compressao de dados para cada requisição - Level Otimizado 
            {
                //options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                //app.UseHsts();
            }

            app.UseResponseCompression();
            //app.UseHttpsRedirection();
            app.UseCors(configurePolicy: x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}