using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace LemonTest.Models
{
    public class NodeDataContext
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        public NodeDataContext()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString);
        }

        public List<Nodemodel> getNodeList(int? NodeId)
        {
            List<Nodemodel> NodeList = new List<Nodemodel>();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("getAllNodes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NodeId", NodeId.handleDBNull());

                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Nodemodel Node = new Nodemodel();

                        Node.NodeId = (int)(reader["NodeId"]);
                        Node.NodeName = reader["NodeName"].ToString();
                        Node.ParentNodeName = reader["ParentNodeName"].ToString();
                        Node.ParentNodeId = (int)reader["ParentNodeId"];
                        Node.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                        Node.StartDate = (DateTime)reader["StartDate"];
                        NodeList.Add(Node);
                    }
                }
            }

            return NodeList;
        }

        public int SaveNode(Nodemodel request)
        {

            int resultCode = 0;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("saveUpdateNode", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NodeId", request.NodeId.handleDBNull());
                cmd.Parameters.AddWithValue("@NodeName", request.NodeName.handleDBNull());
                cmd.Parameters.AddWithValue("@ParentNodeId", request.ParentNodeId.handleDBNull());
                cmd.Parameters.AddWithValue("@IsActive", request.IsActive.handleDBNull());
                cmd.Parameters.AddWithValue("@StartDate", request.StartDate.handleDBNull());

                SqlParameter resultCodeParameter = new SqlParameter("@ResultCode", SqlDbType.Int);
                resultCodeParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultCodeParameter);

                cn.Open();
                cmd.ExecuteNonQuery();

                resultCode = (int)resultCodeParameter.Value;
            }
            return resultCode;
        }

        public List<ParentNodeModel> getParentNodeList(int? CategotyId)
        {
            List<ParentNodeModel> ParentNodeList = new List<ParentNodeModel>();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("getParentNodeList", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ParentNodeId", CategotyId.handleDBNull());

                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ParentNodeModel ParentNode = new ParentNodeModel();

                        ParentNode.ParentNodeId = Convert.ToInt32(reader["ParentNodeId"]);
                        ParentNode.ParentNodeName = reader["ParentNodeName"].ToString();

                        ParentNodeList.Add(ParentNode);
                    }
                }
            }

            return ParentNodeList;
        }

        public int deleteNode(int? NodeId)
        {
            int resultCode = 0;


            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("deleteNode", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NodeId", NodeId.handleDBNull());

                SqlParameter resultCodeParameter = new SqlParameter("@ResultCode", SqlDbType.Int);
                resultCodeParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultCodeParameter);

                cn.Open();
                cmd.ExecuteNonQuery();

                resultCode = (int)resultCodeParameter.Value;
            }
            return resultCode;
        }
    }
}