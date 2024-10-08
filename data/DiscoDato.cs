﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace data
{
    public class DiscoDato
    {
        public List<Disco> listar()
        {
            List<Disco> lista = new List<Disco>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, E.Descripcion Genero, T.Descripcion Formato From DISCOS D, ESTILOS E, TIPOSEDICION T where E.id = D.idEstilo and T.id = D.IdTipoEdicion";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Titulo = (string)lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)lector["CantidadCanciones"];
                    aux.UrlImagenTapa = (string)lector["UrlImagenTapa"];
                    aux.Genero = new Estilo();
                    aux.Genero.Descripcion = (string)lector["Genero"];
                    aux.Formato = new TipoEdicion();
                    aux.Formato.Descripcion = (string)lector["Formato"];

                    lista.Add(aux);


                }

                conexion.Close();
                return lista;
            }

            catch (Exception ex)
            {

                throw ex;
            }

            
        }
    }
}
