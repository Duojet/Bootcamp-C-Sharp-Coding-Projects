﻿using CFApp5.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(CFApp5.Startup))]
namespace CFApp5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup I am creating first Admin Role and creating a default Admin User
            if (!roleManager.RoleExists("Admin"))
            {

                //create Admin role:
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //create Admin superuser 

                var user = new ApplicationUser();
                user.UserName = "admin@email.com";
                user.Email = "admin@email.com";
                user.EmailConfirmed = true;
                user.FirstName = "Super";
                user.LastName = "User";
                string userPWD = "Password1234!";

                var chkUser = UserManager.Create(user, userPWD);

                //add default User to Role Administrator
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }



            //creating Creating Employee role
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }
        }

    }
}