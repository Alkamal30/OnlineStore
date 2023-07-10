using OnlineStore.Core;
using OnlineStore.WebAPI.Filters;
using Hangfire;
using OnlineStore.WebAPI.MappingProfiles;
using OnlineStore.Core.Services;


namespace OnlineStore.WebAPI
{
    public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			builder.Services
				.AddCoreServices(builder.Configuration)
				.AddAutoMapper(
					typeof(MappingProfile),
					typeof(InfrastructureToCoreMappingProfile),
					typeof(CoreToWebApiMappingProfile)
				);

			builder.Services.AddHangfire(
				config => config.UseSqlServerStorage(
					builder.Configuration.GetConnectionString("Hangfire")
				)
			);
			builder.Services.AddHangfireServer();

			builder.Services.AddControllers()
				.AddNewtonsoftJson(options => {
					options.SerializerSettings.ReferenceLoopHandling 
						= Newtonsoft.Json.ReferenceLoopHandling.Ignore;
				});

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddScoped<RequestsLimitFilter>();
			builder.Services.AddStatisticsCollector();

			var app = builder.Build();

			if(app.Environment.IsDevelopment()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors(
				x => x.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowAnyOrigin()
			);
			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseStaticFiles();

			app.MapControllers();
			app.MapHangfireDashboard();

			app.Services.UseStatisticsCollector();

			app.Run();
		}
	}
}