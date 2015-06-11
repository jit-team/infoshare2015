# .NET not only for Windows

Hello! We are happy you taking part in our workshop!

We wish you will learn something about the future of the .NET platform. 

The application we intend to write today is a messaHere ge hub. We have a simple http server setup to which you can _post_ a message using the HTTP protocol.

Here are some code snippets and commands which should be helpful during this workshop.

`dnu restore` Downloads packages from NuGet repositories. This should be done after adding new dependencies to `project.json` while restart VS Code.

`dnx . <task>` for example `dnx . kestrel` or `dnx . run`. Runs the command defined in the `project.json` file.

`dnvm update` downloads the runtime, we will work on `beta4`. Adding the `-u` argument will cause to download the nightly build.

`yo aspnet` will run Yeoman with the aspnet generator. From here you can prep a new project.

You can visit https://www.npmjs.com/package/generator-aspnet to see more goodies.

# Web project

In the web you may notice the `Startup.cs` file. This file operates as an bootstrap for our web application.

In that class you can register and configure basic modules which will be used in your web app.

Add the MVC module
```
public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }
```

Adding error page, and a classic route in MVC
```
public void Configure(IApplicationBuilder app)
        {
            app.UseErrorPage();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
```





