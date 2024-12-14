using Microsoft.AspNetCore.Http;
using SpMedicalGroup.Contexts;
using SpMedicalGroup.Domains;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalGroup.Models
{
    public class EmpresaModel
    {
        SpMedicalGroupContext ctx = new SpMedicalGroupContext();
        public void Cadastrar(Empresa novaEmpresa)
        {
            ctx.Empresas.Add(novaEmpresa);

            ctx.SaveChanges();
        }

        public List<Empresa> ListarTodos()
        {
            return ctx.Empresas.ToList();
        }
    }
}
