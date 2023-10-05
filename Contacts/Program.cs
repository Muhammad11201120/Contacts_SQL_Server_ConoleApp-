using Contacts_BusinessLayer;
using Countries_BusinessLayer;
using System;
using System.Data;

namespace Contacts
{
    internal class Program
    {
        public static void testFindContactByID( int id )
        {
            clsContact contact = clsContact.Find( id );
            if ( contact != null )
            {
                Console.WriteLine( " Contact Name = " + contact.firstName + " " + contact.lastName );
                Console.WriteLine( " Contact email = " + contact.email );
                Console.WriteLine( " Contact Phone = " + contact.phone );
                Console.WriteLine( " Contact Address = " + contact.address );
                Console.WriteLine( " Contact Country = " + contact.countrtID );
                Console.WriteLine( "Contact Date Of Birth = " + contact.dateOfBirth );
                Console.WriteLine( " Contact Image = " + contact.ImagPath );
            }
            else
            {
                Console.WriteLine( "No Record With This ID" );
            }
        }
        public static void testFindCountryByID( int id )
        {
            clsCountries country = clsCountries.Find( id );
            if ( country != null )
            {
                Console.WriteLine( " Contact Name = " + country.CountryName );
            }
            else
            {
                Console.WriteLine( "No Record With This ID" );
            }
        }
        public static void testFindCountryByName( string countryName )
        {
            clsCountries country = clsCountries.Find( countryName );
            if ( country != null )
            {
                Console.WriteLine( " Country Name = " + country.CountryName );
            }
            else
            {
                Console.WriteLine( "No Record With This Country Name" );
            }
        }
        public static void testAddContact()
        {
            clsContact contact = new clsContact();
            contact.firstName = "Salman";
            contact.lastName = "Fahad";
            contact.email = "salman@gmail.com";
            contact.phone = "0503265156";
            contact.address = "KSA-JEDDAH";
            contact.dateOfBirth = new DateTime( 1977, 11, 6, 10, 30, 0 );
            contact.countrtID = 1;
            contact.ImagPath = "";
            if ( contact.Save() )
            {
                Console.WriteLine( "Contact With ID : " + contact.Id + " Add Successfully.." );
            }
            else
            {
                Console.WriteLine( "There is Something Went Wrong.." );
            }
        }
        public static void testUpdateContact( int id )
        {
            clsContact contact = clsContact.Find( id );
            if ( contact != null )
            {
                contact.firstName = "Fahdan";
                contact.lastName = "Rami";
                contact.email = "Fahdan@gmail.com";
                contact.phone = "0503225156";
                contact.address = "KSA-JEDDAH";
                contact.dateOfBirth = new DateTime( 1977, 11, 6, 10, 30, 0 );
                contact.countrtID = 1;
                contact.ImagPath = "";
                if ( contact.Save() )
                {
                    Console.WriteLine( "Contact With ID : " + contact.Id + " Updated Successfully.." );
                }
            }
            else
            {
                Console.WriteLine( "There is Something Went Wrong.." );
            }
        }
        public static void testDeleteContact( int id )
        {
            if ( clsContact.DeleteContact( id ) )
            {
                Console.WriteLine( "Contact Deleted Successfuly.." );
            }
            else
            {
                Console.WriteLine( "this contact infos doesn`t exists.." );
            }
        }
        public static void testShowAllContacts()
        {
            DataTable dataTable = clsContact.findAllList();
            foreach ( DataRow row in dataTable.Rows )
            {
                Console.WriteLine( row[ "ContactID" ] + " | " + row[ "FirstName" ] );
            }
        }
        public static void testIsContactExist( int id )
        {
            if ( clsContact.IsContactExist( id ) )
            {
                Console.WriteLine( "Contact With ID = " + id + " Is FOUND.." );
            }
            else
            {
                Console.WriteLine( "Contact With ID = " + id + " Is Not FOUND " );
            }
        }
        public static void testAddNewCountry()
        {
            clsCountries country = new clsCountries();
            country.CountryName = "SWEED";
            country.countryCode = "SWD";
            country.phoneCode = "696";
            if ( country.Save() )
            {
                Console.WriteLine( "Contact With ID : " + country.CountryID + " Add Successfully.." );
            }
            else
            {
                Console.WriteLine( "There is Something Went Wrong.." );
            }
        }
        public static void testUpdateCountry( int id )
        {
            clsCountries country = clsCountries.Find( id );
            if ( country != null )
            {
                country.CountryName = "SENGAL";
                country.countryCode = "SEN";
                country.phoneCode = "255";
                if ( country.Save() )
                {
                    Console.WriteLine( "Contact With ID : " + country.CountryID + " Updated Successfully.." );
                }
            }
            else
            {
                Console.WriteLine( "There is Something Went Wrong.." );
            }
        }
        public static void testShowAllCountries()
        {
            DataTable dataTable = clsCountries.findAllCountriesList();
            foreach ( DataRow row in dataTable.Rows )
            {
                Console.WriteLine( row[ "CountryID" ] + " | " + row[ "CountryName" ] + " | " + row[ "CountryCode" ] + " | " + row[ "PhoneCode" ] );

            }
        }
        public static void testIsCountryExists( string countryName )
        {
            if ( clsCountries.IsCountryExist( countryName ) )
            {
                Console.WriteLine( "Country = " + countryName + " Is FOUND.." );
            }
            else
            {
                Console.WriteLine( "Country  = " + countryName + " Is Not FOUND " );
            }
        }
        public static void testDeleteCountry( int id )
        {
            if ( clsCountries.DeleteCountry( id ) )
            {
                Console.WriteLine( "Contact Deleted Successfuly.." );
            }
            else
            {
                Console.WriteLine( "this contact infos doesn`t exists.." );
            }
        }
        static void Main( string[] args )
        {

            //testFindContactByID( 1 );
            //testAddContact();
            //testUpdateContact( 1 );
            // testDeleteContact( 5 );
            //testShowAllContacts();
            //testIsContactExist( 3 );
            // testFindCountry(string name);
            //testFindCountryByID( 1 );
            //testFindCountryByName( "USA" );
            //testAddNewCountry();
            //testUpdateCountry( 11 );
            testShowAllCountries();
            //testIsCountryExists( "KsA" );
            //testDeleteCountry( 12 );
            Console.ReadKey();
        }
    }
}
