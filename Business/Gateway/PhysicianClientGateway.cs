﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class PhysicianClientGateway
	{
        public static Domain.Physician GetPhysicianByMasterAccessionNo(string masterAccessionNo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select ph.* " +
                "from tblPhysician ph " +
                "join tblAccessionOrder ao on ph.PhysicianId = ao.PhysicianId " +
                "where ao.MasterAccessionNo = @MasterAccessionNo";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
			Domain.Physician result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			return result;
		}

		public static Domain.Physician GetPhysicianByNpi(string npi)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianByNpi(npi);
#else
			Domain.Physician result = null;
			if (string.IsNullOrEmpty(npi) == false && npi != "0")
			{
				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT * FROM tblPhysician where Npi = @Npi";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.Add("@Npi", SqlDbType.VarChar).Value = npi;
				result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			}
			return result;
#endif
		}

		public static Domain.Physician GetPhysicianByPhysicianId(int physicianId)
		{
#if MONGO
            return PhysicianClientGatewayMongo.GetPhysicianByPhysicianId(physicianId);
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM tblPhysician where PhysicianId = @PhysicianId";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
			Domain.Physician result = PhysicianClientGateway.GetPhysicianFromCommand(cmd);
			return result;
#endif
		}

		private static Domain.Physician GetPhysicianFromCommand(SqlCommand cmd)
		{
			Domain.Physician result = null;
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						result = new Domain.Physician();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();

					}
				}
			}
			return result;
		}
        
		public static Domain.PhysicianCollection GetPhysiciansByName(string firstName, string lastName)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysiciansByName(firstName, lastName);
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.Text;
			if(string.IsNullOrEmpty(firstName))
			{
				cmd.CommandText = "SELECT * FROM tblPhysician where LastName like @LastName + '%' order by LastName, FirstName";
			}
			else
			{
				cmd.CommandText = "SELECT * FROM tblPhysician where FirstName like @FirstName + '%' and LastName like @LastName + '%' order by LastName, FirstName";
				cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
			}
			cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;
			Domain.PhysicianCollection result = PhysicianClientGateway.GetPhysicianCollectionFromCommand(cmd);

			if (result.Count == 0)
			{
				cmd = new SqlCommand();
				if(string.IsNullOrEmpty(firstName))
				{
					cmd.CommandText = "SELECT PhysicianID, ClientId, FirstName, LastName, Active, Address, City, State, Zip, Phone, Fax, OutsideConsult, HPVTest, " +
						"HPVInstructionID, HPVTestToPerformID, FullName, HPVStandingOrderCode, ReportDeliveryMethod, DisplayName, HomeBaseClientId, KRASBRAFStandingOrder, " +
						"VoiceCommand, VoiceCommandIsEnabled, Npi, MiddleInitial, Credentials, UserName, ClientsPhysicianId " +
						"FROM tblPhysician where LastName like @LastName + '%' order by LastName, FirstName";
				}
				else
				{
					cmd.CommandText = "SELECT PhysicianID, ClientId, FirstName, LastName, Active, Address, City, State, Zip, Phone, Fax, OutsideConsult, HPVTest, " +
						"HPVInstructionID, HPVTestToPerformID, FullName, HPVStandingOrderCode, ReportDeliveryMethod, DisplayName, HomeBaseClientId, KRASBRAFStandingOrder, " +
						"VoiceCommand, VoiceCommandIsEnabled, Npi, MiddleInitial, Credentials, UserName, ClientsPhysicianId " +
						"FROM tblPhysician where FirstName like @FirstName + '%' and LastName like @LastName + '%' order by LastName, FirstName";
					cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
				}
				cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;
				result = PhysicianClientGateway.GetPhysicianCollectionFromCommand(cmd);
			}
			return result;
#endif
		}        		

        public static Domain.PhysicianCollection GetPhysiciansByClientId(int clientId)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysiciansByClientId(clientId);
#else
            Domain.PhysicianCollection result = new Domain.PhysicianCollection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select ph.* " +
               "from tblPhysician ph " +
               "join tblPhysicianClient pc on ph.PhysicianId = pc.PhysicianId " +
               "where pc.ClientId = @ClientId order by ph.LastName";   
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
            cmd.CommandType = CommandType.Text;
			result = PhysicianClientGateway.GetPhysicianCollectionFromCommand(cmd);
			return result;
#endif
        }

		private static Domain.PhysicianCollection GetPhysicianCollectionFromCommand(SqlCommand cmd)
		{
			Domain.PhysicianCollection result = new Domain.PhysicianCollection();
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.Physician physician = new Domain.Physician();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physician, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physician);
					}
				}
			}
			return result;
		}

        public static YellowstonePathology.Business.Client.ClientGroupCollection GetClientGroupCollection()
        {
#if MONGO
            return PhysicianClientGatewayMongo.GetClientGroupCollection();
#else
            YellowstonePathology.Business.Client.ClientGroupCollection result = new Client.ClientGroupCollection();
            SqlCommand cmd = new SqlCommand("select * from tblClientGroup order by GroupName");
            cmd.CommandType = CommandType.Text;            

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.ClientGroup clientGroup = new Client.ClientGroup();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientGroup, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(clientGroup);
                    }
                }
            }
            return result;
#endif
        }

		public static YellowstonePathology.Business.Client.Model.Client GetClientByClientId(int clientId)
		{
#if MONGO
            return PhysicianClientGatewayMongo.GetClientByClientId(clientId);
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT c.*, (SELECT * from tblClientLocation where ClientId = c.ClientId for xml path('ClientLocation'), type) ClientLocationCollection " +
				" FROM tblClient c where c.ClientId = @ClientId for xml Path('Client'), type";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
			ClientBuilder builder = new ClientBuilder();
			XElement document = PhysicianClientGateway.GetXElementFromCommand(cmd);
			builder.Build(document);
			return builder.Client;
#endif
		}

		private static XElement GetXElementFromCommand(SqlCommand cmd)
		{
			XElement result = null;
			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (XmlReader xmlReader = cmd.ExecuteXmlReader())
				{
					if (xmlReader.Read() == true)
					{
						result = XElement.Load(xmlReader, LoadOptions.PreserveWhitespace);
					}
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.ClientCollection GetClientsByClientName(string clientName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetClientsByClientName(clientName);
#else
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT c.*, (SELECT * from tblClientLocation where ClientId = c.ClientId for xml path('ClientLocation'), type) ClientLocationCollection " +
                "FROM tblClient c where c.ClientName like @ClientName + '%' for xml Path('Client'), Root('ClientCollection'), type";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = clientName;
            XElement resultElement = PhysicianClientGateway.GetXElementFromCommand(cmd);
            return BuildClientCollection(resultElement);
#endif
		}

        public static Domain.ClientCollection GetClientsByPhysicianId(int physicianId)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetClientsByPhysicianId(physicianId);
#else
            Domain.ClientCollection result = new Domain.ClientCollection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select c.* " +
               "from tblClient c " +
               "join tblPhysicianClient pc on c.ClientId = pc.ClientId " +
               "where pc.PhysicianId = @PhysicianId ";
            cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(client);
                    }
                }
            }
            return result;
#endif
        }

        public static Domain.PhysicianClient GetPhysicianClient(int physicianId, int clientId)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClient(physicianId, clientId);
#else
            Domain.PhysicianClient result = null;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblPhysicianClient where PhysicianId = @PhysicianId and ClientId = @ClientId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = new Domain.PhysicianClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();                        
                    }
                }
            }

            return result;
#endif
		}

		public static View.ClientPhysicianView GetClientPhysicianViewByClientId(int clientId)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetClientPhysicianViewByClientId(clientId);
#else
			View.ClientPhysicianView result = null;
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "with phys as(select p.* from tblPhysician p join tblPhysicianClient pc on p.PhysicianId = pc.PhysicianId where pc.ClientId = @ClientId) " +
				"select c.*," +
				" ( select phys.*" +
				"   from phys order by phys.FirstName for xml Path('Physician'), type) Physicians" +
				" from tblClient c where c.ClientId = @ClientId for xml Path('Client'), root('ClientPhysicianView')";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
			XElement resultElement = PhysicianClientGateway.GetXElementFromCommand(cmd);
			if (resultElement != null)
			{
				result = PhysicianClientGateway.BuildClientPhysicianView(resultElement);
			}
			return result;
#endif
		}

		private static View.ClientPhysicianView BuildClientPhysicianView(XElement sourceElement)
		{
			View.ClientPhysicianView result = new View.ClientPhysicianView();
			XElement clientElement = sourceElement.Element("Client");
			YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(clientElement, result);
			xmlPropertyWriter.Write();

			XElement physiciansElement = clientElement.Element("Physicians");
			if(physiciansElement != null)
			{
				List<XElement> physicianElements = physiciansElement.Elements("Physician").ToList<XElement>();
				foreach (XElement physicianElement in physicianElements)
				{
					Domain.Physician physician = new Domain.Physician();
					YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter1 = new Persistence.XmlPropertyWriter(physicianElement, physician);
					xmlPropertyWriter1.Write();
					result.Physicians.Add(physician);
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection GetPhysicianClientNameCollection(string clientName, string physicianName)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientNameCollection(clientName, physicianName);
#else
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = new YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select pc.PhysicianClientId, pc.ClientId, pc.PhysicianId, c.ClientName, p.FirstName, p.LastName, c.Telephone, c.Fax " +
				"from tblPhysicianClient pc join tblClient c on pc.ClientId = c.ClientId " +
				"join tblPhysician p on pc.PhysicianId = p.PhysicianId " +
				"where p.LastName like @PhysicianName + '%' and c.ClientName like @ClientName + '%' order by c.ClientName, p.FirstName ";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = clientName;
			cmd.Parameters.Add("@PhysicianName", SqlDbType.VarChar).Value = physicianName;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName = new YellowstonePathology.Business.Client.Model.PhysicianClientName();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientName, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientName);
					}
				}
			}
			return result;
#endif
		}

		public static YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection GetPhysicianClientNameCollection(int physicianClientId)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientNameCollection(physicianClientId);
#else
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = new YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select pc.PhysicianClientId, pc.ClientId, pc.PhysicianId, c.ClientName, p.FirstName, p.LastName, c.Telephone, c.Fax " +
				"from tblPhysicianClient pc join tblClient c on pc.ClientId = c.ClientId " +
				"join tblPhysician p on pc.PhysicianId = p.PhysicianId " +
				"where p.PhysicianId = (select physicianId from tblPhysicianClient where PhysicianClientId = @PhysicianClientId) order by c.ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PhysicianClientId", SqlDbType.Int).Value = physicianClientId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName = new YellowstonePathology.Business.Client.Model.PhysicianClientName();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientName, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientName);
					}
				}
			}
			return result;
#endif
		}

        public static YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection GetPhysicianNameViewCollectionByPhysicianLastName(string physicianLastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianNameViewCollectionByPhysicianLastName(physicianLastName);
#else
			YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection result = new YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select ph.PhysicianId, ph.FirstName, ph.LastName, c.Telephone [HomeBasePhone], c.Fax [HomeBaseFax] " +
                "from tblPhysician ph " +
                "left outer join tblClient c on ph.HomeBaseClientId = c.ClientId " +
                "where ph.LastName like @LastName + '%' order by ph.FirstName ";

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = physicianLastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.Model.PhysicianNameView physicianNameView = new YellowstonePathology.Business.Client.Model.PhysicianNameView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianNameView, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianNameView);
                    }
                }
            }
            return result;
#endif
        }

		public static List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> GetPhysicianClientDistributions(int physicianClientId)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientDistributions(physicianClientId);
#else
			List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> result = new List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView>();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "Select pcd.*, c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType " +
				"from tblPhysicianClient pc " +
				"join tblPhysicianClientDistribution pcd on pc.PhysicianClientId = pcd.PhysicianClientId " +
				"join tblPhysicianClient pc2 on pcd.DistributionId = pc2.PhysicianClientId " +
				"join tblClient c on pc2.ClientId = c.ClientId " +
				"join tblPhysician ph on pc2.Physicianid = ph.PhysicianId " +
				"where pc.PhysicianClientId = @PhysicianClientId ";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PhysicianClientId", SqlDbType.Int).Value = physicianClientId;

			using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new YellowstonePathology.Business.Client.Model.PhysicianClientDistribution();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView = new YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView(physicianClientDistribution);
						sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistributionView, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(physicianClientDistributionView);
					}
				}
			}
			return result;
#endif
		}

		public static View.PhysicianClientView GetPhysicianClientView(int physicianId)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientView(physicianId);
#else
            SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select * from tblPhysician where PhysicianId = @PhysicianId;" +
				" select c.* from tblClient c join tblPhysicianClient pc on c.ClientId = pc.ClientId where pc.PhysicianId = @PhysicianId order by ClientName";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
			return BuildPhysicianClientView(cmd);
#endif
        }

		public static View.ClientSearchView GetClientSearchViewByClientName(string clientName)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetClientSearchViewByClientName(clientName);
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select ClientId, ClientName, Address, Telephone, Fax from tblClient where clientName like @ClientName + '%' Order By 2 for xml Path('ClientSearchViewItem'), root('ClientSearchView')";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = clientName;
			View.ClientSearchView results = Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteCollectionCommand<View.ClientSearchView>(cmd, Domain.Persistence.DataLocationEnum.ProductionData);
			if (results == null)
			{
				results = new View.ClientSearchView();
			}
			return results;
#endif
		}

		public static View.ClientLocationViewCollection GetClientLocationViewByClientName(string clientName)
		{
#if MONGO
			return PhysicianClientGatewayMongo.GetClientLocationViewByClientName(clientName);
#else
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "select c.ClientId, c.ClientName, cl.ClientLocationId, cl.Location from tblClientLocation cl join tblClient c on cl.ClientId = c.ClientId where c.ClientName like @ClientName + '%' Order By 2, 4 " +
				"for xml path('ClientLocationView'), type, root('ClientLocationViewCollection')";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = clientName;
			View.ClientLocationViewCollection results = Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteCollectionCommand<View.ClientLocationViewCollection>(cmd, Domain.Persistence.DataLocationEnum.ProductionData);
			if (results == null)
			{
				results = new View.ClientLocationViewCollection();
			}
			return results;
#endif
		}

		private static YellowstonePathology.Business.Client.Model.ClientCollection BuildClientCollection(XElement sourceElement)
        {
			YellowstonePathology.Business.Client.Model.ClientCollection clientCollection = new YellowstonePathology.Business.Client.Model.ClientCollection();
            if (sourceElement != null)
            {
                foreach (XElement clientElement in sourceElement.Elements("Client"))
                {
					ClientBuilder builder = new ClientBuilder();
					builder.Build(clientElement);
					YellowstonePathology.Business.Client.Model.Client client = builder.Client;
                    clientCollection.Add(client);
                }
            }
            return clientCollection;
        }

		private static View.PhysicianClientView BuildPhysicianClientView(SqlCommand cmd)
		{
			View.PhysicianClientView result = new View.PhysicianClientView();
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
					}

					dr.NextResult();

					while (dr.Read())
					{
						YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Clients.Add(client);
					}
				}
			}
			return result;
		}

        public static YellowstonePathology.Business.Client.PhysicianClientCollection GetPhysicianClientListByPhysicianLastName(string physicianLastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientListByPhysicianLastName(physicianLastName);
#else
			YellowstonePathology.Business.Client.PhysicianClientCollection result = new Client.PhysicianClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance, c.FacilityType, ph.NPI " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where ph.LastName like @LastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(@"LastName", SqlDbType.VarChar, 50).Value = physicianLastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.PhysicianClient physicianClient = new Client.PhysicianClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClient);
                    }
                }
            }
            return result;
#endif           
        }

        public static YellowstonePathology.Business.Client.PhysicianClientCollection GetPhysicianClientListByClientPhysicianLastName(string clientName, string physicianLastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientListByClientPhysicianLastName(clientName, physicianLastName);
#else
			YellowstonePathology.Business.Client.PhysicianClientCollection result = new Client.PhysicianClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance, c.FacilityType, ph.NPI " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where c.ClientName like @ClientName + '%' and ph.LastName like @PhysicianLastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50).Value = clientName;
            cmd.Parameters.Add("@PhysicianLastName", SqlDbType.VarChar, 50).Value = physicianLastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.PhysicianClient physicianClient = new Client.PhysicianClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClient);
                    }
                }
            }
            return result;
#endif
        }

        public static YellowstonePathology.Business.Client.PhysicianClientCollection GetPhysicianClientListByClientId(int clientId)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientListByClientId(clientId);
#else
			YellowstonePathology.Business.Client.PhysicianClientCollection result = new Client.PhysicianClientCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select pp.PhysicianClientId, c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.FacilityType, c.DistributionType, c.Fax [FaxNumber], c.LongDistance, ph.NPI " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where c.ClientId = @ClientId order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(@"ClientId", SqlDbType.Int).Value = clientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Client.PhysicianClient physicianClient = new Client.PhysicianClient();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClient, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClient);
                    }
                }
            }
            return result;
#endif
		}

        public static Business.Client.PhysicianClientDistributionCollection GetPhysicianClientDistributionByClientId(int clientId)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientDistributionByClientId(clientId);
#else
			Business.Client.PhysicianClientDistributionCollection result = new Client.PhysicianClientDistributionCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where c.ClientId = @ClientId order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(@"ClientId", SqlDbType.Int).Value = clientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.Client.PhysicianClientDistribution physicianClientDistribution = new Client.PhysicianClientDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClientDistribution);
                    }
                }
            }
            return result;
#endif
        }

        public static Business.Client.PhysicianClientDistributionCollection GetPhysicianClientDistributionByClientPhysicianLastName(string clientName, string physicianLastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientDistributionByClientPhysicianLastName(clientName, physicianLastName);
#else
			Business.Client.PhysicianClientDistributionCollection result = new Client.PhysicianClientDistributionCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where c.ClientName like @ClientName + '%' and ph.LastName like @PhysicianLastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50).Value = clientName;
            cmd.Parameters.Add("@PhysicianLastName", SqlDbType.VarChar, 50).Value = physicianLastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.Client.PhysicianClientDistribution physicianClientDistribution = new Business.Client.PhysicianClientDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClientDistribution);
                    }
                }
            }
            return result;
#endif
        }

        public static Business.Client.PhysicianClientDistributionCollection GetPhysicianClientDistributionByPhysicianFirstLastName(string firstName, string lastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientDistributionByPhysicianFirstLastName(firstName, lastName);
#else
			Business.Client.PhysicianClientDistributionCollection result = new Client.PhysicianClientDistributionCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where ph.FirstName like @FirstName + '%' and ph.LastName like @LastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;            
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.Client.PhysicianClientDistribution physicianClientDistribution = new Business.Client.PhysicianClientDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClientDistribution);
                    }
                }
            }
            return result;
#endif
        }

        public static Business.Client.PhysicianClientDistributionCollection GetPhysicianClientDistributionByPhysicianLastName(string lastName)
        {
#if MONGO
			return PhysicianClientGatewayMongo.GetPhysicianClientDistributionByPhysicianLastName(lastName);
#else
			Business.Client.PhysicianClientDistributionCollection result = new Client.PhysicianClientDistributionCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
                 "from tblClient c " +
                 "join tblPhysicianClient pp on c.clientid = pp.clientid " +
                 "Join tblPhysician ph on pp.PhysicianId = ph.PhysicianId " +
                 "where ph.LastName like @LastName + '%' order by ph.LastName, ph.FirstName, c.ClientName";
            cmd.CommandType = CommandType.Text;            
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Business.Client.PhysicianClientDistribution physicianClientDistribution = new Business.Client.PhysicianClientDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClientDistribution);
                    }
                }
            }
            return result;
#endif
        }
	}
}