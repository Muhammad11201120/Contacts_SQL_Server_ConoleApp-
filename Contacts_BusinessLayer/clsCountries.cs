using Countries_DataAccessLayer;
using System.Data;

namespace Countries_BusinessLayer
{
    public class clsCountries
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int CountryID;
        public string CountryName;
        public string countryCode;
        public string phoneCode;

        private clsCountries( int countryID, string countryName )
        {
            this.CountryID = countryID;
            this.CountryName = countryName;
            this.Mode = enMode.Update;
        }
        public clsCountries()
        {
            this.CountryID = -1;
            this.CountryName = "";
            this.Mode = enMode.AddNew;
        }
        public static clsCountries Find( int id )
        {

            string CountryName = "";

            if ( clsCountriesDataAccess.FindCountryByID( id, ref CountryName ) )
            {
                return new clsCountries( id, CountryName );
            }
            else
            {
                return null;
            }
        }
        public static clsCountries Find( string countryName )
        {

            int id = -1;

            if ( clsCountriesDataAccess.FindCountryByName( ref id, countryName ) )
            {
                return new clsCountries( id, countryName );
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewCountry()
        {
            this.CountryID = clsCountriesDataAccess.AddNewCountry( this.CountryName, this.countryCode, this.phoneCode );
            return ( this.CountryID != -1 );
        }
        private bool _UpdateCountry()
        {
            return clsCountriesDataAccess.UpdateCountry( this.CountryID, this.CountryName, this.countryCode, this.phoneCode );
        }
        public bool Save()
        {
            switch ( Mode )
            {
                case enMode.AddNew:
                    if ( _AddNewCountry() )
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateCountry();
            }
            return false;
        }
        public static bool DeleteCountry( int id )
        {
            if ( clsCountriesDataAccess.DeleteCountry( id ) )
            {
                return true;
            }
            return false;
        }
        public static DataTable findAllCountriesList()
        {
            DataTable dt = new DataTable();
            return clsCountriesDataAccess.findAllCountriesList();
        }
        public static bool IsCountryExist( int id )
        {
            return clsCountries.IsCountryExist( id );
        }
        public static bool IsCountryExist( string countryName )
        {
            return clsCountriesDataAccess.IsCountryExists( countryName );
        }
    }
}
