using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscalonamentoHospitalar.Models;

namespace EscalonamentoHospitalar.Data
{
    public class UsersSeedData
    {

        public static async Task EnsurePopulatedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string ADMIN_USER = "bovelheiro@hotmail.com";
            const string ADMIN_PASSWORD = "123Qwe.";

            const string DIRETORSERVICO_USER = "D001";
            const string DIRETORSERVICO_PASSWORD = "1234";

            const string MEDICO_USER = "M001";
            const string MEDICO_PASSWORD = "1234";

            const string ENFERMEIRO_USER = "E001";
            const string ENFERMEIRO_PASSWORD = "1234";

            //--------------------------------------------------

            if (!await roleManager.RoleExistsAsync("Administrador"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrador"));
            }


            if (!await roleManager.RoleExistsAsync("DiretorServico"))
            {
                await roleManager.CreateAsync(new IdentityRole("DiretorServico"));
            }

            if (!await roleManager.RoleExistsAsync("Medico"))
            {
                await roleManager.CreateAsync(new IdentityRole("Medico"));
            }

            if (!await roleManager.RoleExistsAsync("Enfermeiro"))
            {
                await roleManager.CreateAsync(new IdentityRole("Enfermeiro"));
            }


            //------------------------------------------------------

            /* admin */
            ApplicationUser admin = await userManager.FindByNameAsync(ADMIN_USER);

            if (admin == null)
            {
                admin = new ApplicationUser { UserName = ADMIN_USER };
                await userManager.CreateAsync(admin, ADMIN_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(admin, "Administrador"))
            {
                await userManager.AddToRoleAsync(admin, "Administrador");
            }

            /* diretor Serviço */
            ApplicationUser diretorServicoUser = await userManager.FindByNameAsync(DIRETORSERVICO_USER);

            if (diretorServicoUser == null)
            {
                diretorServicoUser = new ApplicationUser { UserName = DIRETORSERVICO_USER };
                await userManager.CreateAsync(diretorServicoUser, DIRETORSERVICO_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(diretorServicoUser, "DiretorServico"))
            {
                await userManager.AddToRoleAsync(diretorServicoUser, "DiretorServico");
            }

            /* medico */
            ApplicationUser medicoUser = await userManager.FindByNameAsync(MEDICO_USER);

            if (medicoUser == null)
            {
                medicoUser = new ApplicationUser { UserName = MEDICO_USER };
                await userManager.CreateAsync(medicoUser, MEDICO_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(medicoUser, "Medico"))
            {
                await userManager.AddToRoleAsync(medicoUser, "Medico");
            }

            /* enfermeiro */
            ApplicationUser enfermeiroUser = await userManager.FindByNameAsync(ENFERMEIRO_USER);

            if (enfermeiroUser == null)
            {
                enfermeiroUser = new ApplicationUser { UserName = ENFERMEIRO_USER };
                await userManager.CreateAsync(enfermeiroUser, ENFERMEIRO_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(enfermeiroUser, "Enfermeiro"))
            {
                await userManager.AddToRoleAsync(enfermeiroUser, "Enfermeiro");
            }
        }



    }
}
