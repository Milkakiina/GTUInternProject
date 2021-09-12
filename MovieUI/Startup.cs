using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieDL;
using MovieDL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject
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
            services.AddControllersWithViews();

            services.AddTransient<IMovieRepo, MovieRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //CHANGE THiS FOR START PAGE
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            Movie m1 = new Movie()
            {
                Title = MovieData.titleList[0],
                Director = MovieData.directorList[0],
                Genre = MovieData.genreList[0],
                ReleaseDate = MovieData.releaseList[0],
            };
            Entities.movieList.Add(m1);

            Entities.movieActorList.Add(new MovieActor()
            {
                movieID = m1.ID,
                actorID = Entities.actorList[0].ID
            });
            Entities.movieActorList.Add(new MovieActor()
            {
                movieID = m1.ID,
                actorID = Entities.actorList[1].ID
            });
            Entities.movieActorList.Add(new MovieActor()
            {
                movieID = m1.ID,
                actorID = Entities.actorList[2].ID
            });
          
        }
    }
}
