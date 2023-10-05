using Contacts_DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;
namespace Countries_DataAccessLayer

{
    public class clsCountriesDataAccess
    {
        public static bool FindCountryByID( int id, ref string CountryName )
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection( DataAccessSettings.Country_conString );
            string query = "SELECT * FROM Countries WHERE CountryID = @id";
            SqlCommand cmd = new SqlCommand( query, connection );
            cmd.Parameters.AddWithValue( "@id", id );

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if ( reader.Read() )
                {
                    isFound = true;
                    CountryName = ( string ) reader[ "CountryName" ];
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch ( System.Exception ex )
            {

                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool FindCountryByName( ref int id, string countryName )
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection( DataAccessSettings.Country_conString );
            string query = "SELECT * FROM Countries WHERE CountryName = @countryName";
            SqlCommand cmd = new SqlCommand( query, connection );
            cmd.Parameters.AddWithValue( "@countryName", countryName );

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if ( reader.Read() )
                {
                    isFound = true;
                    id = ( int ) reader[ "CountryID" ];
                    countryName = ( string ) reader[ "CountryName" ];
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch ( System.Exception ex )
            {

                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static int AddNewCountry( string countryName, string countryCode, string phoneCode )
        {
            // this function returns int so we will store it in this variable
            int countryID = -1;

            SqlConnection connection = new SqlConnection( DataAccessSettings.Country_conString );
            string query = @"INSERT INTO Countries (CountryName) 
                            VALUES 
                            (@countryName,@CountryCode,@PhoneCode);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand( query, connection );
            cmd.Parameters.AddWithValue( "@countryName", countryName );
            cmd.Parameters.AddWithValue( "@countryCode", countryCode );
            cmd.Parameters.AddWithValue( "@phoneCode", phoneCode );

            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();
                if ( result != null && int.TryParse( result.ToString(), out int insertedID ) )
                {
                    countryID = insertedID;
                }
            }
            catch ( System.Exception ex )
            {

                //do nothing
            }
            finally
            {
                connection.Close();
            }
            return countryID;
        }
        public static bool UpdateCountry( int id, string countryName, string countryCode, string phoneCode )
        {
            int rowaAffected = 0;
            SqlConnection connection = new SqlConnection( DataAccessSettings.Country_conString );
            string query = @"UPDATE Countries SET 
                            CountryName = @countryName,
                            CountryCode = @countryCode,
                            PhoneCode = @phoneCode
                       
                            WHERE CountryID = @id";

            SqlCommand cmd = new SqlCommand( query, connection );
            cmd.Parameters.AddWithValue( "@id", id );
            cmd.Parameters.AddWithValue( "@countryName", countryName );
            cmd.Parameters.AddWithValue( "@countryCode", countryCode );
            cmd.Parameters.AddWithValue( "@phoneCode", phoneCode );
            try
            {
                connection.Open();
                rowaAffected = cmd.ExecuteNonQuery();

            }
            catch ( System.Exception ex )
            {

                return false;
            }
            finally
            {
                connection.Close();
            }
            return ( rowaAffected > 0 ); ;
        }
        public static bool DeleteCountry( int id )
        {
            int rowaAffected = 0;
            SqlConnection connection = new SqlConnection( DataAccessSettings.Country_conString );
            string query = "DELETE FROM Countries WHERE CountryID = @id";

            SqlCommand cmd = new SqlCommand( query, connection );
            cmd.Parameters.AddWithValue( "@id", id );
            try
            {
                connection.Open();
                rowaAffected = cmd.ExecuteNonQuery();
            }
            catch ( System.Exception ex )
            {

                return false;
            }
            finally
            {
                connection.Close();
            }
            return ( rowaAffected > 0 );

        }
        public static DataTable findAllCountriesList()
        {
            DataTable dt = new DataTable();
            SqlConnection connect = new SqlConnection( DataAccessSettings.Country_conString );
            string query = "SELECT * FROM Countries";
            SqlCommand cmd = new SqlCommand( query, connect );
            try
            {
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if ( reader.HasRows )
                {
                    dt.Load( reader );
                }
                else
                {
                    dt = null;
                }

            }
            catch ( Exception ex )
            {

                //
            }
            finally
            {
                connect.Close();
            }
            return dt;
        }
        public static bool IsCountryExists( int id )
        {
            bool isFound = false;
            SqlConnection connect = new SqlConnection( DataAccessSettings.Country_conString );
            string query = "SELECT FOUND = 1 FROM Countries WHERE CountryID = @id";
            SqlCommand cmd = new SqlCommand( query, connect );
            cmd.Parameters.AddWithValue( "@id", id );
            try
            {
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch ( Exception ex )
            {
                isFound = false;
            }
            finally
            {
                connect.Close();
            }
            return isFound;
        }
        public static bool IsCountryExists( string countryName )
        {
            bool isFound = false;
            SqlConnection connect = new SqlConnection( DataAccessSettings.Country_conString );
            string query = "SELECT FOUND = 1 FROM Countries WHERE CountryName = @countryName";
            SqlCommand cmd = new SqlCommand( query, connect );
            cmd.Parameters.AddWithValue( "@countryName", countryName );
            try
            {
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch ( Exception ex )
            {
                isFound = false;
            }
            finally
            {
                connect.Close();
            }
            return isFound;
        }
    }

}
