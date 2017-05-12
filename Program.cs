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

                                Console.WriteLine("moi ban nhap id can sua");
                                u.Id = Console.ReadLine();
                                Console.WriteLine("id ban muon update la: " + u.Id);
                                Console.WriteLine("moi ban nhap Account moi");
                                u.Account = Console.ReadLine();
                                Console.WriteLine("moi ban nhap Password moi");
                                u.Password = Console.ReadLine();
                                Console.WriteLine("moi ban nhap Phone moi");
                                u.Phone = Console.ReadLine();
                                model.updateUser(u);
                                Console.WriteLine("insert thanh cong");
                                break;
                            case 4:

                                Console.WriteLine(" moi ban nhap id can xoa");
                                u.Id = Console.ReadLine();
                                model.deleteUser(u);
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
            Console.Read();
        }
    }

    class UserModel
    {

        private MySqlConnection conn;
        private MySqlCommand commad;
        private MySqlDataReader reader;

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
        public void updateUser(User u)
        {
            commad = conn.CreateCommand();
            commad.CommandText = "UPDATE user SET account='" + u.Account + "',password='" + u.Password + "',phone='" + u.Phone + "' WHERE id = '" + u.Id + "'";
            commad.ExecuteNonQuery();
        }

        public void deleteUser(User u)
        {
            commad = conn.CreateCommand();
            commad.CommandText = "DELETE FROM user WHERE id = '" + u.Id + "' ";
            commad.ExecuteNonQuery();
        }
    }
}
