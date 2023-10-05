using Contacts_DataAccessLayer;
using System;
using System.Data;

namespace Contacts_BusinessLayer
{

    public class clsContact
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int countrtID { get; set; }
        public string ImagPath { get; set; }

        public clsContact()
        {
            this.Id = -1;
            this.firstName = "";
            this.lastName = "";
            this.email = "";
            this.phone = "";
            this.address = "";
            this.dateOfBirth = DateTime.Now;
            this.countrtID = -1;
            this.ImagPath = "";
            Mode = enMode.AddNew;
        }
        private clsContact( int id, string firstName, string lastName, string email, string phone, string address, DateTime dateOfBirth, int countrtID, string imagPath )
        {
            this.Id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phone = phone;
            this.address = address;
            this.dateOfBirth = dateOfBirth;
            this.countrtID = countrtID;
            this.ImagPath = imagPath;
            Mode = enMode.Update;
        }
        public static clsContact Find( int id )
        {
            string firstName = "", lastName = "", email = "", phone = "", address = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            int countrtID = -1;
            if ( clsContactsDataAccsses.FindContactByID( id, ref firstName, ref lastName, ref email, ref phone, ref address, ref dateOfBirth, ref countrtID, ref imagePath ) )
            {
                return new clsContact( id, firstName, lastName, email, phone, address, dateOfBirth, countrtID, imagePath );
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewContact()
        {
            this.Id = clsContactsDataAccsses.AddNewContact( this.firstName, this.lastName, this.email, this.phone, this.address, this.dateOfBirth, this.countrtID, this.ImagPath );
            return ( this.Id != -1 );
        }
        private bool _UpdateContact()
        {
            return clsContactsDataAccsses.UpdateContact( this.Id, this.firstName, this.lastName, this.email, this.phone, this.address, this.dateOfBirth, this.countrtID, this.ImagPath );
        }
        public static bool DeleteContact( int id )
        {
            if ( clsContactsDataAccsses.DeleteContact( id ) )
            {
                return true;
            }
            return false;
        }
        public bool Save()
        {
            switch ( Mode )
            {
                case enMode.AddNew:
                    if ( _AddNewContact() )
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateContact();
            }
            return false;
        }
        public static DataTable findAllList()
        {
            DataTable dt = new DataTable();
            return clsContactsDataAccsses.findAllList();
        }
        public static bool IsContactExist( int id )
        {
            return clsContactsDataAccsses.IsContactExist( id );
        }
    }
}
