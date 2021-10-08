using Sistemas.Core.Entities.Models;
using Sistemas.Core.Exceptions;
using Sistemas.Core.Interfaces.Models;
using Sistemas.Data.Repositories.Sqlservers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Sistemas.Data.Repositories.Models
{
    public class BancoRepository : ConexionRepository, IBancoRepository
    {
        public BancoRepository(SqlConnection context, SqlTransaction transaction) : base(context, transaction)
        {

        }
        public void Create(Banco t)
        {
            SQLDatos_SGS.Ejecutar("SM_Registro_Banco", t.Id,t.Glosa, t.Sw, t.Abreviatura);

        }

        public Banco Get(int id)
        {
            var tb = SQLDatos_SGS.TraerDataReader("SM_Lista_Bancos",id);

            //try
            //{
            //    tb.Read();

            //    return new Banco
            //    {
            //        Id = Convert.ToInt32(tb["codigo"]),
            //        Glosa = tb["glosa"].ToString(),
            //        Sw = Convert.ToBoolean(tb["sw"]),
            //        Abreviatura = tb["abrevia"].ToString()
            //    };

            //}
            //catch (Exception ex)
            //{

            //    string error = ex.Message; //acción para manejar el error;

            //}

            if (!tb.Read())
            {
                throw new BusinessException("User doesn't exist");
            }

            return new Banco
            {
                Id = Convert.ToInt32(tb["codigo"]),
                Glosa = tb["glosa"].ToString(),
                Sw = Convert.ToBoolean(tb["sw"]),
                Abreviatura = tb["abrevia"].ToString()
            };



        }

        public IEnumerable<Banco> GetAll()
        {
            var tb = SQLDatos_SGS.TraerDataTable("SM_Lista_Bancos");

            return tb.AsEnumerable().Select(b =>
            {
                return new Banco
                {
                    Id = Convert.ToInt32(b["codigo"]),
                    Glosa = b["glosa"].ToString(),
                    Sw = Convert.ToBoolean(b["sw"]),
                    Abreviatura = b["abrevia"].ToString()
                };
            });

            //List<DataRow> bancos = SQLDatos_SGS.TraerDataTable("").AsEnumerable().ToList();
            //IEnumerable<DataRow> bancos= SQLDatos_SGS.TraerDataTable("CB_Lista_Bancos").AsEnumerable();

            //return (IEnumerable<Banco>)bancos;
        }

        public void Update(Banco t)
        {
            SQLDatos_SGS.Ejecutar("SM_Registro_Banco", t.Id, t.Glosa, t.Sw, t.Abreviatura);
        }
    }
}
