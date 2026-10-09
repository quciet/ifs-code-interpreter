Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.HttpsPolicy
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Options
Imports IFs.Core
Imports IFs.Data

Public Class Startup

    Public Sub New(configuration As IConfiguration)
        Configuration = configuration
    End Sub

    Public ReadOnly Property Configuration As IConfiguration

    ' This method gets called by the runtime. Use this method to add services to the container.
    Public Sub ConfigureServices(services As IServiceCollection)
        services.AddScoped(Of IQueryService, QueryService)()
        services.AddScoped(Of IGraphService, GraphService)()
        services.AddScoped(Of IForecastService, ForecastService)()
        services.AddScoped(Of IStorageService, StorageService)()
        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
    End Sub

    ' This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    Public Sub Configure(app As IApplicationBuilder, env As IHostingEnvironment)
        If env.IsDevelopment() Then
            app.UseDeveloperExceptionPage()
        Else
            app.UseHsts()
        End If
        app.UseHttpsRedirection()
        app.UseMvc()
    End Sub

End Class
