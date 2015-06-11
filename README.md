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


#Tag helpers

In beta4 to use tag helpers, you need to do the following:

1. Add `"Microsoft.AspNet.Mvc.TagHelpers": "6.0.0-beta4"` to dependencies in `project.json`
2. Create a `Views/_GlobalImport.cshtml` file with content off `@addTagHelper "*, Microsoft.AspNet.Mvc.TagHelpers
3. Voila!

Number 2 is specific for `beta4`, in later version that was changed.

Now you can use the new ASP MVC tag helpers

```
@model Infoshare.Workshop.Webapp.Models.LoginForm

<form asp-action="LoginAction">

    <div class="form-group">
        <label asp-for="Login">Name:</label>
        <input type="text" asp-for="Login" />
    </div>
    <button type="submit" class="btn btn-default">Submit</button>
</form>
```

Take a look at the `asp-*` attributes, those are bindings which are compiled and bound from the model.







