using SpMedicalGroup.Contexts;
using SpMedicalGroup.Domains;


namespace SpMedicalGroup.Models
{
    public class RoleModel
    {
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();
        public void Cadastrar(Role novaRole)
        {
            ctx.Roles.Add(novaRole);
            ctx.SaveChanges();
        }

        public List<Role> ListarTodas()
        {
            return ctx.Roles.ToList();
        }
    }
}
