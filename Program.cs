using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConnectMySQl
{
    class User
    {

        private string account;
        private string password;
        private string phone;
        private String id;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        public string Account
        {
            get { return account; }
            set { account = value; }
        }


        public string Id
        {
            get { return id; }
            set { id = value; }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            UserModel model = new UserModel();
            try
            {
                Console.WriteLine("ket noi thanh cong");
                {
                    User u = new User();
                    int choice;
                    do
                    {
                        Console.WriteLine("----------Menu----------");
                        Console.WriteLine("1. them moi user");
                        Console.WriteLine("2. hien thi user");
                        Console.WriteLine("3. sua user");
                        Console.WriteLine("4. xoa user");
                        Console.WriteLine("5. thoat");
                        Console.WriteLine("moi chon: ");
                        choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("ban chon them moi user:");
                                Console.WriteLine("moi ban nhap account");
                                u.Account = Console.ReadLine();
                                Console.WriteLine("moi ban nhap password");
                                u.Password = Console.ReadLine();
                                Console.WriteLine("moi ban nhap phone");
                                u.Phone = Console.ReadLine();
                                model.addUser(u);
                                break;
                            case 2:
                                Console.WriteLine("ban chon hien thi su lieu: ");
                                model.selectUser();
                                break;
                            case 3:
                                Console.WriteLine("Nhap id muon sua: ");
                                string nid = Console.ReadLine();
                                // Lay ra trong database, kiem tra su ton tai.
                                User existUser = model.getUserById(nid);
                                if (existUser == null) {
                                    Console.WriteLine("Khong ton tai nguoi dung voi id: " + nid);
                                    break;
                                }
                                Console.WriteLine("Ton tai thong tin nguoi dung voi id: " + nid);
                                Console.WriteLine("Account: " + existUser.Account);
                                Console.WriteLine("Password: " + existUser.Password);
                                Console.WriteLine("Phone: " + existUser.Phone);

                                User userUpdate = new User();
                                userUpdate.Id = nid;
                                Console.WriteLine("Vui long nhap thong tin moi: ");
                                Console.WriteLine("Account: ");
                                userUpdate.Account = Console.ReadLine();
                                Console.WriteLine("Password: ");
                                userUpdate.Password = Console.ReadLine();
                                Console.WriteLine("Phone: ");
                                userUpdate.Phone = Console.ReadLine();
                                model.conn.Open();
                                model.updateUser(userUpdate);
                                Console.WriteLine("Update thanh cong!");
                                break;
                            case 4:
                                Console.WriteLine("Nhap id can xoa: ");
                                String idx = Console.ReadLine();
                                User existUser1 = model.getUserById1(idx);
                                if (existUser1 == null)
                                {
                                    Console.WriteLine("Khong ton tai nguoi dung voi id: " + idx);
                                    break;
                                }
                                Console.WriteLine("Ton tai thong tin nguoi dung voi id: " + idx);
                                Console.WriteLine("Account: " + existUser1.Account);
                                Console.WriteLine("Password: " + existUser1.Password);
                                Console.WriteLine("Phone: " + existUser1.Phone);
                                model.getUserById1(idx);
                                model.deleteUser(idx);
                                break;
                            case 5:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("lua chon khong hop le moi ban chon lai!");
                                break;
                        }
                    }
                    while (choice != 5);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: ", ex.Message);
            }
            Console.ReadLine();
        }
    }

    class UserModel
    {

        public MySqlConnection conn;
        public MySqlCommand commad;
        public MySqlDataReader reader;

        public UserModel()
        {
            String connString = "Server=localhost;Database=c1608l;Port=3306;Username=root;Password=";
            conn = new MySqlConnection(connString);
            conn.Open();
        }

        public void addUser(User u)
        {
            commad = conn.CreateCommand();
            commad.CommandText = "insert into user (account,password,phone) values('" + u.Account + "','" + u.Password + "','" + u.Phone + "')";
            commad.ExecuteNonQuery();
            Console.WriteLine("insert thanh cong");
        }

        public void selectUser()
        {
            commad = conn.CreateCommand();
            commad.CommandText = "SELECT * FROM user";
            reader = commad.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("id la: " + reader["id"]);
                Console.WriteLine("account la: " + reader["account"]);
                Console.WriteLine("password la: " + reader["password"]);
                Console.WriteLine("phone la: " + reader["phone"]);
            }
        }
        public void updateUser(User userUpdate)
        {
            commad = conn.CreateCommand();
            commad.CommandText = "UPDATE user SET account='" + userUpdate.Account + "',password='" + userUpdate.Password + "',phone='" + userUpdate.Phone + "' WHERE id = '" + userUpdate.Id + "'";
            commad.ExecuteNonQuery();
            Console.WriteLine("uppdate thanh cong");
        }
        public User getUserById(String nid)
        {
            commad = conn.CreateCommand();
            commad.CommandText = "SELECT * FROM user WHERE id = '" + nid + "'";
            MySqlDataReader reader = commad.ExecuteReader();
            User u = null;
            if (reader.Read())
            {
                u = new User();
                u.Account = Convert.ToString(reader["account"]);
                u.Password = Convert.ToString(reader["password"]);
                u.Phone = Convert.ToString(reader["phone"]);
            }            
            conn.Close();
            return u;
        }
        public User getUserById1(String idx)
        {
            commad = conn.CreateCommand();
            commad.CommandText = "SELECT * FROM user WHERE id = '" + idx + "'";
            MySqlDataReader reader = commad.ExecuteReader();
            User u = null;
            if (reader.Read())
            {
                u = new User();
                u.Account = Convert.ToString(reader["account"]);
                u.Password = Convert.ToString(reader["password"]);
                u.Phone = Convert.ToString(reader["phone"]);
            }
            conn.Close();
            return u;
        }
        public void deleteUser(String idx)
        {
            commad = conn.CreateCommand();
            commad.CommandText = "DELETE FROM user WHERE id = '" + idx + "' ";
            commad.ExecuteNonQuery();
        }
    }
}
