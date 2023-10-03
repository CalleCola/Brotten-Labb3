using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Windows.Input;
using System.Data.SqlTypes;
using Microsoft.VisualBasic;

namespace Brotten.Models
{
    public class IntagsMetoder
    {
        public IntagsMetoder() { }

        public int InsertIntagen(Tbl_Intagen ig, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = Studenter; Integrated Security = True";

            String sqlstring = "INSERT INTO Tbl_Intagen (Fornamn, Efternamn) VALUES (@Fornamn, @Efternamn)";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("@Fornamn", SqlDbType.NVarChar, 30).Value = ig.Fornamn;
            dbCommand.Parameters.Add("@Efternamn", SqlDbType.NVarChar, 30).Value = ig.Efternamn;
            dbCommand.Parameters.Add("Intagen_nr", SqlDbType.Int).Value = ig.Intagen_nr;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "Det skapas inte en ny intagen i databasen"; }
                return i;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }

        }
        public int DeleteIntagen(Tbl_Intagen ig, out string errormsg)
        {
            if (string.IsNullOrEmpty(ig.Fornamn) || ig.Intagen_nr == 0)
            {
                errormsg = "Please provide both Fornamn and Intagen_nr.";
                return 0; // Return 0 to indicate an error
            }
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = Studenter; Integrated Security = True";

            String sqlstring = "DELETE FROM Tbl_Intagen WHERE Fornamn = @Fornamn AND Intagen_nr = @Intagen_nr";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("@Fornamn", SqlDbType.NVarChar, 30).Value = ig.Fornamn;
            dbCommand.Parameters.Add("@Intagen_nr", SqlDbType.Int).Value = ig.Intagen_nr;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = " "; }
                else { errormsg = "Det togs inte bort någon från "; }
                return i;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }
        public int DeleteBtn(Tbl_Intagen ig, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = Studenter; Integrated Security = True";

            String sqlstring = "DELETE FROM Tbl_Intagen WHERE Intagen_nr = @Intagen_nr";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("@Intagen_nr", SqlDbType.Int).Value = ig.Intagen_nr;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = " "; }
                else { errormsg = "Det togs inte bort någon från "; }
                return i;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }
        public int Uppdatera(Tbl_Intagen ig, out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = Studenter; Integrated Security = True";

            String sqlstring = "UPDATE Tbl_Intagen SET Fornamn = @Fornamn, Efternamn = @Efternamn WHERE Intagen_nr = @Intagen_nr";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("@Fornamn", SqlDbType.NVarChar, 30).Value = ig.Fornamn;
            dbCommand.Parameters.Add("@Efternamn", SqlDbType.NVarChar, 30).Value = ig.Efternamn; 
            dbCommand.Parameters.Add("@Intagen_nr", SqlDbType.Int).Value = ig.Intagen_nr;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = " "; }
                else { errormsg = "Det togs inte bort någon från "; }
                return i;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }

        }
        public List<Tbl_Intagen> SearchIntagna(SökModell searchKriterie)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = Studenter; Integrated Security = True";

            String sqlstring = "SELECT * FROM Tbl_Intagen WHERE Efternamn = @Efternamn AND Fornamn = @Fornamn ORDER BY Efternamn ASC";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("@Efternamn", SqlDbType.NVarChar, 30).Value = searchKriterie.Efternamn;
            dbCommand.Parameters.Add("@Fornamn", SqlDbType.NVarChar, 30).Value = searchKriterie.Fornamn;

            List<Tbl_Intagen> result = new List<Tbl_Intagen>();

            try
            {
                dbConnection.Open();

                SqlDataReader reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    Tbl_Intagen ig = new Tbl_Intagen();
                    ig.Fornamn = reader["Fornamn"].ToString();
                    ig.Efternamn = reader["Efternamn"].ToString();
                    ig.Intagen_nr = Convert.ToInt16(reader["Intagen_nr"]);
                    result.Add(ig);
                }
            }
            catch (Exception e)
            {
               
            }
            finally
            {
                dbConnection.Close();
            }

            return result;
        }
        public List<Tbl_Intagen> FilterIntagna(string filterBy, string filterValue)
        {
            List<Tbl_Intagen> result = new List<Tbl_Intagen>();

            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = Studenter; Integrated Security = True";

                String sqlstring = "SELECT * FROM Tbl_Intagen WHERE 1=1";

                if (!string.IsNullOrEmpty(filterValue))
                {
                    sqlstring += $" AND {filterBy} LIKE @FilterValue";
                }

                SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

                if (!string.IsNullOrEmpty(filterValue))
                {
                    dbCommand.Parameters.Add("@FilterValue", SqlDbType.NVarChar, 30).Value = "%" + filterValue + "%";
                }

                try
                {
                    dbConnection.Open();

                    SqlDataReader reader = dbCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Tbl_Intagen ig = new Tbl_Intagen();
                        ig.Fornamn = reader["Fornamn"].ToString();
                        ig.Efternamn = reader["Efternamn"].ToString();
                        ig.Intagen_nr = Convert.ToInt16(reader["Intagen_nr"]);
                        result.Add(ig);
                    }
                }
                catch (Exception e)
                {

                }
            }

            
            if (filterBy == "Fornamn")
            {
                result = result.OrderBy(x => x.Fornamn).ToList();
            }
            else if (filterBy == "Efternamn")
            {
                result = result.OrderBy(x => x.Efternamn).ToList();
            }
            else if (filterBy == "Intagen_nr")
            {
                result = result.OrderBy(x => x.Intagen_nr).ToList();
            }

            return result;
        }
        public List<Tbl_Intagen> GetIntagenWithDataSet(out string errormsg)
        {

            SqlConnection dbConnection = new SqlConnection();
            
            dbConnection.ConnectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = Studenter; Integrated Security = True";

            String sqlstring = "SELECT * FROM Tbl_Intagen";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<Tbl_Intagen> Register= new List<Tbl_Intagen>();

            errormsg = "";

            try
            {
                dbConnection.Open();

                myAdapter.Fill(myDS, "myPerson");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myPerson"].Rows.Count;

                if (count > 0)
                {
                    while (i < count) // Changed loop condition
                    {
                        Tbl_Intagen ig = new Tbl_Intagen();
                        ig.Fornamn = myDS.Tables["myPerson"].Rows[i]["Fornamn"].ToString();
                        ig.Efternamn = myDS.Tables["myPerson"].Rows[i]["Efternamn"].ToString();
                        ig.Intagen_nr = Convert.ToInt16(myDS.Tables["myPerson"].Rows[i]["Intagen_nr"]);
                        ig.Brott_Typ = myDS.Tables["myPerson"].Rows[i]["Brott_Typ"].ToString();

                        i++;
                        Register.Add(ig);
                    }
                    errormsg = "";
                    return Register;
                }
                else
                {
                    errormsg = "Det hämtas ingen Intagen";
                    return null;
                }
              

            }catch (Exception e) 
            {
                errormsg = e.Message;
                return null;
                            
            }
            
        }

        public List<string> GetDistinctBrottTyper()
        {
            List<string> brottTyper = new List<string>();

            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = Studenter; Integrated Security = True";


                string sqlstring = "SELECT DISTINCT Brott_Typ FROM Tbl_Brott";
                SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);



                SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
                DataSet myDS = new DataSet();

                try
                {
                    dbConnection.Open();

                    SqlDataReader reader = dbCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        string brottTyp = reader["Brott_Typ"].ToString(); // Assuming Brott_Typ is a column in your database
                        brottTyper.Add(brottTyp);
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            return brottTyper;
        }
        public List<Tbl_Intagen> FilterByCategory(string brottTyp)
        {
            List<Tbl_Intagen> result = new List<Tbl_Intagen>();

            using (SqlConnection dbConnection = new SqlConnection())
            {
                
                dbConnection.ConnectionString = "Data Source = (localdb)\\mssqllocaldb; Initial Catalog = Studenter; Integrated Security = True";

                String sqlstring = "SELECT * FROM Tbl_Intagen WHERE Brott_Typ = @BrottTyp";

                SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);



                dbCommand.Parameters.Add("@BrottTyp", SqlDbType.NVarChar, 30).Value = brottTyp;


                try
                {
                    dbConnection.Open();

                    SqlDataReader reader = dbCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Tbl_Intagen ig = new Tbl_Intagen();
                        ig.Fornamn = reader["Fornamn"].ToString();
                        ig.Efternamn = reader["Efternamn"].ToString();
                        ig.Intagen_nr = Convert.ToInt16(reader["Intagen_nr"]);
                        ig.Brott_Typ = reader["Brott_Typ"].ToString(); 
                        result.Add(ig);
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            return result;
        }

        public List<Tbl_Intagen> SearchById(int id)
        {
            List<Tbl_Intagen> searchResults = new List<Tbl_Intagen>();

            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Studenter;Integrated Security=True";

                string sqlstring = "SELECT * FROM Tbl_Intagen WHERE Intagen_nr = @ID";
                SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
                dbCommand.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                try
                {
                    dbConnection.Open();
                    SqlDataReader reader = dbCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Tbl_Intagen ig = new Tbl_Intagen();
                        ig.Intagen_nr = Convert.ToInt32(reader["Intagen_nr"]);
                        ig.Fornamn = reader["Fornamn"].ToString();
                        ig.Efternamn = reader["Efternamn"].ToString();
                        ig.Brott_Typ = reader["Brott_Typ"].ToString();
                        searchResults.Add(ig);
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            return searchResults;
        }
















    }
}
