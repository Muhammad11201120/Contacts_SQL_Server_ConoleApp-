using System;
using System.Data;
using System.Data.SqlClient;

namespace Contacts_DataAccessLayer
{
    public static class clsContactsDataAccsses
    {
        public static bool FindContactByID( int id, ref string firstName, ref string lastName, ref string email, ref string phone, ref string address, ref DateTime dateOfBirth, ref int country_ID, ref string ImgPath )
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection( DataAccessSettings.Contact_conString );
            string query = "SELECT * FROM Contacts WHERE ContactID = @id";
            SqlCommand cmd = new SqlCommand( query, connection );
            cmd.Parameters.AddWithValue( "@id", id );

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if ( reader.Read() )
                {
                    isFound = true;
                    firstName = ( string ) reader[ "FirstName" ];
                    lastName = ( string ) reader[ "LastName" ];
                    email = ( string ) reader[ "Email" ];
                    phone = ( string ) reader[ "Phone" ];
                    address = ( string ) reader[ "Address" ];
                    dateOfBirth = ( DateTime ) reader[ "DateOfBirth" ];
                    country_ID = ( int ) reader[ "CountryID" ];

                    // Handling Nullble Values In DataBase
                    if ( reader[ "ImagePath" ] != DBNull.Value )
                    {
                        ImgPath = ( string ) reader[ "ImagePath" ];
                    }
                    else
                    {
                        ImgPath = string.Empty;
                    }
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
        public static int AddNewContact( string firstName, string lastName, string email, string phone, string address, DateTime dateOfBirth, int country_ID, string imgPath )
        {
            // this function returns int so we will store it in this variable
            int contactID = -1;

            SqlConnection connection = new SqlConnection( DataAccessSettings.Contact_conString );
            string query = @"INSERT INTO Contacts (FirstName,LastName,Email,Phone,Address,DateOfBirth,CountryID,ImagePath) 
                            VALUES 
                            (@firstName,@lastName,@email,@phone,@address,@dateOfBirth,@countryID,@imagePath);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand( query, connection );
            cmd.Parameters.AddWithValue( "@firstName", firstName );
            cmd.Parameters.AddWithValue( "@lastName", lastName );
            cmd.Parameters.AddWithValue( "@email", email );
            cmd.Parameters.AddWithValue( "@phone", phone );
            cmd.Parameters.AddWithValue( "@address", address );
            cmd.Parameters.AddWithValue( "@dateOfBirth", dateOfBirth );
            cmd.Parameters.AddWithValue( "@countryID", country_ID );

            if ( imgPath != "" )
            {
                cmd.Parameters.AddWithValue( "@imagePath", imgPath );
            }
            else
            {
                cmd.Parameters.AddWithValue( "@imagePath", System.DBNull.Value );
            }


            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();
                if ( result != null && int.TryParse( result.ToString(), out int insertedID ) )
                {
                    contactID = insertedID;
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
            return contactID;
        }
        public static bool UpdateContact( int id, string firstName, string lastName, string email, string phone, string address, DateTime dateOfBirth, int country_ID, string imgPath )
        {
            int rowaAffected = 0;
            SqlConnection connection = new SqlConnection( DataAccessSettings.Contact_conString );
            string query = @"UPDATE Contacts SET 
                            FirstName = @firstName,
                            LastName = @lastName ,
                            Email= @email,
                            Phone= @phone,
                            Address= @address,
                            DateOfBirth= @dateOfBirth,
                            CountryID= @countryID,
                            ImagePath= @imagePath 
                            WHERE ContactID = @id";

            SqlCommand cmd = new SqlCommand( query, connection );
            cmd.Parameters.AddWithValue( "@id", id );
            cmd.Parameters.AddWithValue( "@firstName", firstName );
            cmd.Parameters.AddWithValue( "@lastName", lastName );
            cmd.Parameters.AddWithValue( "@email", email );
            cmd.Parameters.AddWithValue( "@phone", phone );
            cmd.Parameters.AddWithValue( "@address", address );
            cmd.Parameters.AddWithValue( "@dateOfBirth", dateOfBirth );
            cmd.Parameters.AddWithValue( "@countryID", country_ID );

            if ( imgPath != "" )
            {
                cmd.Parameters.AddWithValue( "@imagePath", imgPath );
            }
            else
            {
                cmd.Parameters.AddWithValue( "@imagePath", System.DBNull.Value );
            }


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
        public static bool DeleteContact( int id )
        {
            int rowaAffected = 0;
            SqlConnection connection = new SqlConnection( DataAccessSettings.Contact_conString );
            string query = "DELETE FROM Contacts WHERE ContactID = @id";

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
        public static DataTable findAllList()
        {
            DataTable dt = new DataTable();
            SqlConnection connect = new SqlConnection( DataAccessSettings.Contact_conString );
            string query = "SELECT * FROM Contacts";
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
        public static bool IsContactExist( int id )
        {
            bool isFound = false;
            SqlConnection connect = new SqlConnection( DataAccessSettings.Contact_conString );
            string query = "SELECT FOUND = 1 FROM Contacts WHERE ContactID = @id";
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
    }
}
